/**
 * @author Sagi Bernstein
 */
 
 //*******************************************Constants
var NULL_VALUE =				0x0000;
var OK = 						0x0001;
var USER_NOT_FOUND = 			0x0002;
var POST_NOT_FOUND = 			0x0004;
var SUB_FORUM_NOT_FOUND = 		0x0010;
var ENTRY_EXISTS =				0x0020;
var INSUFFICENT_PERMISSIONS = 	0x0040;
var ADMIN_PERMISSIONS_NEEDED = 	0x0100;
var SECURITY_ERROR = 			0x0200;
var POLICY_REJECTED = 			0x0400;
var ILLEGAL_POST = 				0x1000;
var GUEST = 					0x0;
var MEMBER =					0x1;
var MODERATOR =					0x2;
var ADMIN = 					0x4;

 //*******************************************Variables
var username = 			"guest";
var password;
var query;
var level = 			GUEST;
var currentPost =		null;
var currentSubforum = 	null;
var recursionLevel =	1;
var idCounter =			0;
var subscribeRequest;


//*******************************************The Ajax Call
function callServiceWithError(methodName, params, onSuccess, onError)
{
				
	return $.ajax({	url: "ServerNetworkAdaptor.svc/" + methodName,
				contentType: "application/json",
				data: JSON.stringify(params),
				dataType: "json",
				success: onSuccess,
				error: onError,
				type: "POST"
			});
}

function callService(methodName, params, onSuccess)
{	
	return callServiceWithError(methodName, params, onSuccess, 
		function(req, msg, obj){alert("Lost connection with the server")});
}

//*******************************************User Functions
function RegisterAndLoginCall(user, methodName)
{
	var response = callService(methodName, user,
		function(result){
		//alert("Result is: " + result[methodName + "Result"]);
		var res = result[methodName + "Result"];
		switch(res){
			case OK:
			case MEMBER:
			case MODERATOR:
			case ADMIN:
				if(methodName == "Register"){
					if(res == OK)
						RegisterAndLogin("Login");
					else break;
				}
				else{
					$('input[name="username"]').val('');
					$('input[name="password"]').val('');
					level = res;
					$('form[name="login"]').fadeOut('fast',
						function(){
							$('form[name="login"]').hide();
							$('form[name="logout"]').html('welcome ' + username + 
							'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<button name="logoutButton" type="button" onclick="Logout(username)" class="login-out-buttons">logout</button>');
							$('form[name="logout"]').fadeIn('fast');
							refresh();
					});
				}
				break;
			case NULL_VALUE:
			case GUEST:if(methodName == "Login" & (username != "")){
					alert("check your username and password");
					break;
					}
					alert("user name and password cannot be empty");
				break;
			case SECURITY_ERROR: if(methodName == "Register"){
					alert("user already exists");
					}
					else alert("user already logged in");
				break;
			}
		}
	);
}

function RegisterAndLogin(methodName)
{
	var name = $('input[name="username"]').val();
	var pass = $('input[name="password"]').val();
	username = name;
	password = pass;
	subscribeRequest.abort();
	subscribeRequest = Subscribe();
	return RegisterAndLoginCall({"username": name, "password": pass}, methodName);
}

function Logout(name)
{
	var methodName = "Logout";
	callService(methodName, {"username": name},
		function(result){
		//alert("Result is: " + result[methodName + "Result"]);
		if(result[methodName + "Result"] == 1){
			$('form[name="logout"]').fadeOut('fast', 
					function(){
						password = null;
						username = "guest";
						subscribeRequest.abort();
						subscribeRequest = Subscribe();
						refresh();
						$('form[name="login"]').fadeIn('fast');
					}
			);
		}
		}
	);
}

function Subscribe()
{
	return callServiceWithError("Subscribe", {"username": username},
		function(result)
		{
			if(result.SubscribeResult != null)
				alert(JSON.stringify(result.SubscribeResult.Key.Username + " posted on " + result.SubscribeResult.Subforum));
			subscribeRequest = Subscribe();
		},
		function(req, msg, obj){}
	);
}

//*******************************************Viewing Functions
function Search()
{
	query = $('input[name="query"]').val();
	callService("Search", {"query": query},
			function(result)
			{
				$('input[name="query"]').val('');
				if(result.SearchResult.length > 0){
					ClearPage();
					ShowPosts(result.SearchResult);
				}
				else alert('no results match ' + query);
				query = "";
			});
}

function GetSubforumsList()
{
	currentSubforum = null;
	currentpost = null;
	var response = callService("GetSubforumsList", "", function(result){
	var sfList = $('#subforumsTable');
	sfList.hide();
	ClearPage();
	
		$.each(result, function(i)
			{
				var tr = document.createElement("tr");
				var td = document.createElement("td");
				td.innerHTML = result[i];
				tr.appendChild(td);
				td.setAttribute('onclick', 'GetSubforum(\'' + result[i] + '\')');
				td.setAttribute('class', 'subforum');
				td.setAttribute('colspan', '2');
				sfList.append(tr);
			}
		)
		sfList.fadeIn('slow');
		});
}

function refresh()
{
	if(currentPost)
		GetReplies(currentPost.Key.Username + ',' + currentPost.Key.Time);
	else if(currentSubforum)
		GetSubforum(currentSubforum);
	else if(!$('.subforum').exists()) 
		Search(query)
		
}

function ShowPosts(posts)
{

	$.each(posts, function(i)
		{
			var tr = document.createElement("tr");
			var td = document.createElement("td");
			var post = posts[i];
			var id = idCounter++;
			var buttons = "";
			if(post.HasReplies)
				buttons = '<button id="repliesB' + id + '" class="postButton" onclick="GetReplies(\'' + post.Key.Username + ',' + post.Key.Time + '\')" >view replies</button>';
			
			if(username != "guest")
				buttons = buttons + '<button id="replyB' + id + '" class="postButton" onclick="showReply(\'' + post.Key.Username + ',' + post.Key.Time + ',' + id + '\')" >reply</button>'
			+ '<button id="editB' + id + '" class="postButton" onclick="showEdit(\'' + post.Key.Username + ',' + post.Key.Time + ',' + id + '\')" >edit</button>'
			+ '<button id="removeB' + id + '" class="postButton" onclick="Remove(\'' + post.Key.Username + ',' + post.Key.Time + ',' + id + '\')" >remove</button>';
			
			var buttonsTr = '<tr colspan="3"><td>' + buttons + '</td></tr>';
			td.innerHTML = '<div><table id="post' + id + '" width="800px"><tbody><tr>' +
			'<td class="postTitle">' + post.Title + '</td><td>' + 
			'</td><td class="postPoster"> posted by ' + post.Key.Username + ' on ' + getDateString(post.Key.Time) + '</td></tr>' +
			'<tr><td class="postContent" colspan="3"><h2>' + post.Body + '</h2></td></tr>'
			+ buttonsTr + '</tbody></table></div>';
			tr.appendChild(td);
			td.setAttribute('class', 'post');
			$('#subforumsTable').append(tr);
		}
	);
	$('post').fadeIn();
}

function GetSubforum(name)
{
	ClearPage();
	currentPost = null;
	currentSubforum = name;
	var table = document.createElement("table");
	var tr = document.createElement("tr");
	var tr2 = document.createElement("tr");
	tr2.setAttribute('class','titleTr');
	var tdTitle = document.createElement("td");
	var tdpost = document.createElement("td");
	tdpost.setAttribute('align', 'center');
	tdTitle.setAttribute('align', 'center');
	tdTitle.setAttribute('class', 'post');
	tdpost.setAttribute('class', 'post');
	tdTitle.innerHTML = '<h1>' + name + '</h1>';
	tdpost.innerHTML = '<button id="subforumpostbutton" class="postButton" onclick="showPost(\'' + name + '\')" >post</button>';
	tr.appendChild(tdTitle);
	if(username != "guest")
		tr.appendChild(tdpost);
	table.appendChild(tr);
	tr2.appendChild(table);
	$('#subforumsTable').append(tr2);
	
	callService("GetSubforum", {"subforum": name},
			function(result)
			{
				
				ShowPosts(result.GetSubforumResult);
			}
	);
}
 
function ClearPage()
{
	
	$('.post').fadeOut();
	$('.post').parent().remove();
	$('.subforum').fadeOut();
	$('.titleTr').remove();
	$('.subforum').parent().remove();
}

function GoUp()
{
	ClearPage();
	if (currentPost != null &&
		currentPost.ParentPost != null)
		{
			//currentPost = currentPost.ParentPost;
			callService("GetPost", {"postkey": { "Username" : currentPost.ParentPost.Username, "Time" : currentPost.ParentPost.Time}},
				function(result)
				{
					currentPost = result.GetPostResult;
				}
			);
			GetReplies(currentPost.ParentPost.Username + ',' + currentPost.ParentPost.Time);
		}
	else if(currentPost != null)
		{
			GetSubforum(currentPost.Subforum);
		}
	else
		{
			GetSubforumsList();
		}
}

function GetReplies(postKey)
{	
	
	var splitted = postKey.split(",");
	callService("GetPost", {"postkey": { "Username" : splitted[0], "Time" : splitted[1]}},
			function(result)
			{
				currentPost = result.GetPostResult;
				callService("GetReplies", {"postkey": { "Username" : splitted[0], "Time" : splitted[1]}},
					function(result)
					{
						ClearPage();
						ShowPosts(result.GetRepliesResult);
					}
				);
			}
	);
}

 //*******************************************Posting Functions
function showPost(subforum)
{
	var postHtml = '<tr><td colspan="2"><div class="post" id="posting' + subforum + '" >\
					title:</br><textarea id="titleToPost' + subforum + '" rows="1" cols="70"/></br>'
					 + 'body:</br><textarea id="bodyToPost' + subforum + '" rows="10" cols="70" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
					 + '<button id="cancelBtn" class="postButton" onclick="cancelPost(\'' + subforum + '\')" >cancel</button>'
					 + '</div></td></tr>';
	$('.titleTr').children().append(postHtml);
	$('#subforumpostbutton').attr("onclick", 'doPost(\'' + subforum + '\')');
	$('#subforumpostbutton').html("submit");
	$('#posting' + subforum).hide();
	$('#posting' + subforum).slideDown('slow');
	
}

function cancelPost(subforum)
{
	$('#subforumpostbutton').attr('onclick', 'showPost(\'' + subforum + '\')');
	$('#subforumpostbutton').html("post");
	$('#posting' + subforum).slideUp('slow', 
		function(){$('#posting' + subforum).parent().parent().remove();}
	);
}

function doPost(subforum)
{
	cancelPost(subforum);
	callService("Post", {"current": subforum,
						"toPost" : { "Key": { "Username" : username, "Time" : '\/Date(' + new Date().getTime() + '+0300)\/'},
							"Title": $('#titleToPost' + subforum).val(), "Body": $('#bodyToPost' + subforum).val(),
							"Parent": null,
							"Subforum": subforum }
						},
			function(result)
			{
				switch(result.PostResult)
				{
					case OK:
						refresh();
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
}

function rollDown(postKey, method)
{
	var splitted = postKey.split(",");
	var id = splitted[2];
	var postHtml = '<div id="posting' + id + '"><tr><td><div>title:</br><textarea id="titleToPost' + id + '" rows="1" cols="80"/></div></td></tr>'
		+ '<tr><td><div>body:</br><textarea id="bodyToPost' + id + '" rows="10" cols="80" />'+
		'<button class="postButton" onclick="cancel' + method + '(\'' + postKey + '\')">cancel</button></div></td></tr><div>';
	$('#post'+id).children().append(postHtml);
	$('#posting' + id).hide();
	$('#posting' + id).slideDown('slow');
	if(typeof $('#repliesB'+id) !== 'undefined')
		$('#repliesB'+id).attr("disabled", "disabled");
}

function showReply(postKey)
{
	var splitted = postKey.split(",");
	var id = splitted[2];
	rollDown(postKey, "Reply");
	$('#replyB'+id).html('submit');
	$('#replyB'+id).hide();
	$('#replyB'+id).show();
	$('#replyB'+id).attr("onclick", 'doReply(\'' + postKey + '\')');
	$('#editB'+id).attr("disabled", "disabled");
	$('#removeB'+id).attr("disabled", "disabled");

}

function cancelReply(postKey)
{
	var splitted = postKey.split(",");
	var id = splitted[2];
	var sub = currentSubforum;
	$('#posting' + id).slideUp('slow', function(){$('#posting' + id).remove();});
	if(typeof $('#repliesB'+id) !== 'undefined')
		$('#repliesB'+id).attr("disabled", "false");
	$('#replyB'+id).html('reply');
	$('#replyB'+id).attr('onclick', 'showReply(\'' + postKey + '\')');
	$('#replyB'+id).hide();
	$('#replyB'+id).show();
	$('#editB'+id).removeAttr("disabled");
	$('#removeB'+id).removeAttr("disabled");
}

function doReply(postKey)
{
	cancelReply(postKey);
	var splitted = postKey.split(",");
	var id = splitted[2];
	var sub = currentSubforum;
	
	callService("Reply", {"current": { "Username" : splitted[0], "Time" : splitted[1]},
							"toPost" : { "Key": { "Username" : username, "Time" : '\/Date(' + new Date().getTime() + '+0300)\/'},
							"Title": $('#titleToPost' + id).val(), "Body": $('#bodyToPost' + id).val(),
							"Parent": { "Username" : splitted[0], "Time" : splitted[1]},
							"Subforum": currentSubforum }
						},
			function(result)
			{
				switch(result.ReplyResult)
				{
					case OK:
						$('#post'+splitted[2]).parent().slideUp('slow', function(){$('#posting' + id).remove();});
						refresh();
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
}

function showEdit(postKey)
{
	var splitted = postKey.split(",");
	var id = splitted[2];
	rollDown(postKey, "Edit");
	$('#editB'+id).html('submit');
	$('#editB'+id).hide();
	$('#editB'+id).show();
	$('#editB'+id).attr("onclick", 'doEdit(\'' + postKey + '\')');
	$('#replyB'+id).attr("disabled", "disabled");
	$('#removeB'+id).attr("disabled", "disabled");
	$('#titleToPost' + id).val($('#post' + id).find('.postTitle').html());
	$('#bodyToPost' + id).val($('#post' + id).find('h2').html());
}

function cancelEdit(postKey)
{
	var splitted = postKey.split(",");
	var id = splitted[2];
	var sub = currentSubforum;
	$('#posting' + id).slideUp('slow', function(){$('#posting' + id).remove();});
	if(typeof $('#repliesB'+id) !== 'undefined')
		$('#repliesB'+id).attr("disabled", "false");
	$('#editB'+id).html('edit');
	$('#editB'+id).attr('onclick', 'showEdit(\'' + postKey + '\')');
	$('#editB'+id).hide();
	$('#editB'+id).show();
	$('#replyB'+id).removeAttr("disabled");
	$('#removeB'+id).removeAttr("disabled");
}

function doEdit(postKey)
{	
	cancelEdit(postKey);
	var splitted = postKey.split(",");
	var id = splitted[2];
	var sub = currentSubforum;
	callService("EditPost", {"oldPost": { "Username" : splitted[0], "Time" : splitted[1]},
							"newPost" : { "Key": null,
							"Title": $('#titleToPost' + id).val(), "Body": $('#bodyToPost' + id).val(),
							"Parent": null,
							"Subforum": sub },
							"username":username,
							"password":password
						},
			function(result)
			{
				switch(result.EditPostResult)
				{
					case OK:
						$('#post'+splitted[2]).parent().slideUp('slow', function(){$('#posting' + id).remove();});
						refresh();
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
}

function Remove(postKey)
{
	var splitted = postKey.split(",");
	callService("RemovePost", {"postkey": { "Username" : splitted[0], "Time" : splitted[1]}, "username":username, "password":password},
			function(result)
			{
				switch(result.RemovePostResult)
				{
					case OK:
						$('#post'+splitted[2]).parent().slideUp('slow', function(){$(toRemove).parent().parent().remove();});
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
}

//*******************************************Utils
jQuery.fn.exists = function(){return this.length>0;}

function getDateString(jsonDate)
{
     if (jsonDate == undefined) {
         return "";
     }
     var utcTime = parseInt(jsonDate.substr(6));

     var date = new Date(utcTime);

     return date.toString("dd/MM/yyyy HH:mm:ss");
	 
 }
 
 
 
 
 //*******************************************AdminTools
function GetSubforumsListAdminTools()
{
	var response = callService("GetSubforumsList", "", function(result){
	$('#removeSubforumDiv').height(0);
	$('#subforumsTableAdminTool').empty();
	var sfList = $('#subforumsTableAdminTool');
	sfList.hide();
		$.each(result, function(i)
			{
				var tr = document.createElement("tr");
				var td = document.createElement("td");
				td.innerHTML = result[i];
				tr.appendChild(td);
				td.setAttribute('onclick', 'RemoveSubforum(\'' + result[i] + '\')');
				td.setAttribute('class', 'subforumsList');
				td.setAttribute('style','cursor: pointer;');
				td.setAttribute('colspan', '2');
				sfList.append(tr);
				$('#removeSubforumDiv').height($('#removeSubforumDiv').height() + 28);
			}
		)
		sfList.fadeIn('slow');
		});
}

function AddSubforum()
{
	var subforum = $('input[name="subforumToAddText"]').val();
	callService("AddSubforum", {"username": username,
						"password" : password,
						"subforum": subforum},
			function(result)
			{
				switch(result.PostResult)
				{
					case OK:
						alert("Subforum was added successfully!");
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
	GetSubforumsListAdminTools();
}

function RemoveSubforum(subforum)
{
	callService("RemoveSubforum", {"username": username,
						"password" : password,
						"subforum": subforum},
			function(result)
			{
				switch(result.PostResult)
				{
					case OK:
						alert("Subforum was deleted successfully!");
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
	//$('#subforumsTableAdminTool').detach();
	GetSubforumsListAdminTools();
	
}

function GetModeratosList()
{
	var response = callService("GetSubforumsList", "", function(result){
		$('#moderatorsTableDiv').height(0);
		$('#moderatorsTable').empty();
		$('#moderatorButtonsTable').empty();
		var sfList = $('#moderatorsTable');
		sfList.hide();
			$.each(result, function(i)
				{
					var tr = document.createElement("tr");
					var td = document.createElement("td");
					td.innerHTML = result[i];
					tr.appendChild(td);
					td.setAttribute('style','cursor: pointer;');
					td.setAttribute('onclick', 'AddModeratorToSubforum(\'' + result[i] + '\')');
					td.setAttribute('class', 'subforumsList');
					td.setAttribute('colspan', '2');
					var response2 = callService("GetModerators", {"subforum" : result[i]}, function(result2){
						$.each(result2, function(j)
						{
							var td2 = document.createElement("td");
							td2.innerHTML = result2[j];
							tr.appendChild(td2);
							td2.setAttribute('onclick', 'ModeratorChange(\'' + result2[j] + '\' , \'' + result[i] + '\')');
							td2.setAttribute('class', 'moderatorsList');
							td2.setAttribute('style','cursor: pointer;');
							td2.setAttribute('colspan', '2');
						})
					})
					sfList.append(tr);
					$('#moderatorsTableDiv').height($('#moderatorsTableDiv').height() + 28);
				}
			)
			$('#moderatorsTableDiv').height($('#moderatorsTableDiv').height() + 200);
			sfList.fadeIn('slow');
			});
}

function ModeratorChange(moderator, subforum)
{
	$('#moderatorButtonsTable').empty();
	var mbList = $('#moderatorButtonsTable');
	mbList.hide();
	var td1 = "<td>";// = document.createElement("td");
	td1 += "<div class=\"moderatorInstructions\">";
	td1 += "<br> Subforum: " + subforum + "<br>Replace moderator \'" + moderator + "\' with:";
	td1 += "<input name=\"newModeratorRep\" type=\"text\" placeholder=\"New Moderator Name\">";
	td1 += "<button name=\"replaceModeratorButton\" type=\"button\" onclick=\"ReplaceModerator(\'"+moderator+"\' , \'" + subforum + "\')\" class=\"login-out-buttons\">Replace Moderator</button>";//TODO
	td1 += "</div>";
	td1 += "</td>";
	var td2 = "<td> OR ";//document.createElement("td");
	td2 += "<button name=\"deleteModerator\" type=\"button\" onclick=\"DeleteModerator(\'"+moderator+"\' , \'" + subforum + "\')\" class=\"login-out-buttons\">Delete Moderator "+moderator+"</button>"
	td2 += "</td>";
	var tr1 = "<tr>" + td1 + "</tr>";
	var tr2 = "<tr>" + td2 + "</tr>";
	//tr1.appendChild(td2);
	mbList.append(tr1);
	mbList.append(tr2);
	mbList.fadeIn('slow');
}

function AddModeratorToSubforum(subforum)
{
	$('#moderatorButtonsTable').empty();
	var mbList = $('#moderatorButtonsTable');
	mbList.hide();
	var td1 = "<td>";
	td1 += "<div class=\"moderatorInstructions\">";
	td1 += "<br> Subforum: " + subforum + "<br>New moderator name:";
	td1 += "<input name=\"newModerator\" type=\"text\" placeholder=\"New Moderator Name\">";
	td1 += "<button name=\"newModeratorButton\" type=\"button\" onclick=\"AddNewModerator(\'" + subforum + "\')\" class=\"login-out-buttons\">Add Moderator</button>";
	td1 += "</td>";
	var tr1 = "<tr>" + td1 + "</tr>";
	mbList.append(tr1);
	mbList.fadeIn('slow');
}

function DeleteModerator(moderator, subforum)
{
	callService("RemoveModerator", {"username": username,
						"password" : password,
						"userNameToAdd" : moderator,
						"subforum": subforum},
			function(result)
			{
				switch(result.PostResult)
				{
					case OK:
						alert("Subforum was added successfully!");
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
	GetModeratosList();
}


function AddNewModerator(subforum)
{
	var newModerator = $('input[name="newModerator"]').val();
	callService("AddModerator", {"username": username,
						"password" : password,
						"userNameToAdd" : newModerator,
						"subforum": subforum},
			function(result)
			{
				switch(result.PostResult)
				{
					case OK:
						alert("Subforum was added successfully!");
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
	GetModeratosList();
}

function ReplaceModerator(oldModerator, subforum)
{
	var newModerator = $('input[name="newModeratorRep"]').val();
	callService("AddModerator", {"username": username,
						"password" : password,
						"userNameToAdd" : newModerator,
						"userToRemove" : oldModerator,
						"subforum": subforum},
			function(result)
			{
				switch(result.PostResult)
				{
					case OK:
						alert("Subforum was added successfully!");
						break;
					default:
						alert("insufficient permissions!");
						break;
				}
			}
	);
	GetModeratosList();
}


