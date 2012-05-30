/**
 * @author Sagi Bernstein
 */
 
 //*******************************************Variables
var username = "guest";
var password;
var currentPost = null;
var currentSubforum = null;
var recursionLevel = 1;
var idCounter = 0;

var NULL_VALUE = 0x0000;
var OK = 0x0001;
var USER_NOT_FOUND = 0x0002;
var POST_NOT_FOUND = 0x0004;
var SUB_FORUM_NOT_FOUND = 0x0010;
var ENTRY_EXISTS = 0x0020;
var INSUFFICENT_PERMISSIONS = 0x0040;
var ADMIN_PERMISSIONS_NEEDED = 0x0100;
var SECURITY_ERROR = 0x0200;
var POLICY_REJECTED = 0x0400;
var ILLEGAL_POST = 0x1000;
 
 
//*******************************************The Ajax Call
function callService(methodName, params, onSuccess) {
				
	return $.ajax({	url: "ServerNetworkAdaptor.svc/" + methodName,
				contentType: "application/json",
				data: JSON.stringify(params),
				dataType: "json",
				success: onSuccess,
				error: function(req, msg, obj){alert("Lost connection with the server")},
				type: "POST"
			});
}

//*******************************************User Functions
function RegisterAndLoginCall(user, methodName){
	var response = callService(methodName, user,
		function(result){
		//alert("Result is: " + result[methodName + "Result"]);
		switch(result[methodName + "Result"]){
			case OK:
				if(methodName == "Register"){
					RegisterAndLogin("Login");
				}
				else{
					$('input[name="username"]').val('');
					$('input[name="password"]').val('');
					$('form[name="login"]').fadeOut('fast',
						function(){
							$('form[name="logout"]').html('<p>logged in as ' + username + 
							' <button name="logoutButton" type="button" onclick="Logout(username)" class="login-out-buttons">logout</button></p>');
							$('form[name="logout"]').fadeIn('fast');
							refresh();
					});
				}
				break;
			case NULL_VALUE:
			case USER_NOT_FOUND:if(methodName == "Login" & username != ""){
					alert("user " + username + " is not registered");
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

function RegisterAndLogin(methodName){
	var name = $('input[name="username"]').val();
	var pass = $('input[name="password"]').val();
	username = name;
	password = pass;
	return RegisterAndLoginCall({"username": name, "password": pass}, methodName);
}

function Logout(name){
	var methodName = "Logout";
	callService(methodName, {"username": name},
		function(result){
		//alert("Result is: " + result[methodName + "Result"]);
		if(result[methodName + "Result"] == 1){
			$('form[name="logout"]').fadeOut('fast', 
					function(){
						password = null;
						username = "guest";
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
	callService("Subscribe", {"username": username},
		function(result)
		{
			if(result.SubscribeResult != null)
				alert(JSON.stringify(result.SubscribeResult));
			//TODO: do somthing else
			setTimeout('Subscribe()',60 * 1000);
		}
	);
}

//*******************************************Viewing Functions
function GetSubforumsList(){
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
				buttons = '<button "class="postButton" onclick="GetReplies(\'' + post.Key.Username + ',' + post.Key.Time + '\')" >view replies</button>';
			
			if(username != "guest")
				buttons = buttons + '<button class="postButton" onclick="showReply(\'' + post.Key.Username + ',' + post.Key.Time + ',' + id + '\')" >reply</button>'
			+ '<button class="postButton" onclick="showEdit(\'' + post.Key.Username + ',' + post.Key.Time + ',' + id + '\')" >edit</button>'
			+ '<button class="postButton" onclick="Remove(\'' + post.Key.Username + ',' + post.Key.Time + ',' + id + '\')" >remove</button>';
			
			var buttonsTr = '<tr colspan="3"><td>' + buttons + '</td></tr>';
			td.innerHTML = '<div><table id="post' + id + '" width="800px"><tbody><tr><td class="postTitle">' + post.Title + '</td><td>' + 
			'</td><td class="postPoster"> posted by ' + post.Key.Username + ' on ' + getDateString(post.Key.Time) + '</td></tr>' +
			'<tr><td class="postContent" colspan="3"><h2>' + post.Body + '</h2></td></tr>' + buttonsTr + '</tbody></table></div>';
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
	tdpost.innerHTML = '<button class="postButton" onclick="showPost(\'' + name + '\')" >post</button>';
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
		currentPost.Parent != null)
		{
			currentPost = currentPost.Parent;
			GetReplies(currentPost.Parent);
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
	callService("GetPost", {"postkey": { "username" : splitted[0], "time" : splitted[1]}},
			function(result)
			{
				currentPost = result.GetPostResult;
				currentSubforum = null;
			}
	);
	callService("GetReplies", {"postkey": { "username" : splitted[0], "time" : splitted[1]}},
			function(result)
			{
				ClearPage();
				ShowPosts(result.GetSubforumResult);
			}
	);
}

 //*******************************************Posting Functions
function showPost(subforum)
{
	var splitted = postKey.split(",");
	$('#post'+splitted[2]).append('<h1>oh yehhhh!!</h1>');
	//TODO
}


function showReply(postKey)
{
	var splitted = postKey.split(",");
	$('#post'+splitted[2]).append('<h1>oh yehhhh!!</h1>');
	//TODO
}

function showEdit(postKey)
{
	var splitted = postKey.split(",");
	$('#post'+splitted[2]).append('<h1>oh yehhhh!!</h1>');
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
 function getDateString(jsonDate) {
     if (jsonDate == undefined) {
         return "";
     }
     var utcTime = parseInt(jsonDate.substr(6));

     var date = new Date(utcTime);
     var minutesOffset = date.getTimezoneOffset();

     return date.addMinutes(minutesOffset).toString("dd/MM/yyyy hh:mm:ss");
 }