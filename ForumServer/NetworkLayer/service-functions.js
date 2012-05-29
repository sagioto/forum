/**
 * @author Sagi Bernstein
 */
var username = "guest";
var currentPost;
var recursionLevel = 1;
 
 
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

function GetSubforumsList(){
	var response = callService("GetSubforumsList", "", function(result){
		var sfList = $('#subforumsTable');
		sfList.hide();
		$.each(result, function(i)
			{
				var tr = document.createElement("tr");
				var td = document.createElement("td");
				td.innerHTML = result[i];
				tr.appendChild(td);
				td.setAttribute('onclick', 'GetSubforum(\'' + result[i] + '\')');
				td.setAttribute('class', 'subforum');
				sfList.append(tr);
			}
		)
		sfList.fadeIn('slow');
		});
}

function RegisterAndLoginCall(user, methodName){
	var response = callService(methodName, user,
		function(result){
		//alert("Result is: " + result[methodName + "Result"]);
		switch(result[methodName + "Result"]){
			case 1:
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
					});
				}
				break;
			case 0:
			case 2:if(methodName == "Login" & username != ""){
					alert("user " + username + " is not registered");
					break;
					} 
				alert("user name and password cannot be empty");
				break;
			case 512: if(methodName == "Register"){
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
				alert(JSON.stringify(result));
		}
	);
	setTimeout('Subscribe()',60 * 1000);
}

function GetSubforum(name)
{
	callService("GetSubforum", {"subforum": name},
		function(result)
		{
			$('td').fadeOut();
			$.each(result.GetSubforumResult, function(i)
				{
					var tr = document.createElement("tr");
					var td = document.createElement("td");
					var post = result.GetSubforumResult[i];
					td.innerHTML = '<table width="800px"><tbody><tr><td class="postTitle">' + post.Title + '</td>' + 
					'<td class="postPoster"> posted by ' + post.Key.Username + ' on ' + getDateString(post.Key.Time) + '</td></tr>' +
					'<tr><td class="postContent" colspan="2"><h2>' + post.Body + '</h2></td><tr></tbody></table>';
					tr.appendChild(td);
					td.setAttribute('onclick', 'GetReplies(\'' + post.Key + '\')');
					td.setAttribute('class', 'post');
					$('#subforumsTable').append(tr);
				}
			);
		}
	);
}

function getDateString(jsonDate) {
     if (jsonDate == undefined) {
         return "";
     }
     var utcTime = parseInt(jsonDate.substr(6));

     var date = new Date(utcTime);
     var minutesOffset = date.getTimezoneOffset();

     return date.addMinutes(minutesOffset).toString("dd/MM/yyyy hh:mm:ss");
 }