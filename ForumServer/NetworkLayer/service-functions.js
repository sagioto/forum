/**
 * @author Sagi Bernstein
 */
function callService(methodName, params, onSuccess) {
				
	return $.ajax({	url: "ServerNetworkAdaptor.svc/" + methodName,
				contentType: "application/json",
				data: JSON.stringify(params),
				dataType: "json",
				success: onSuccess,
				error: function(req, msg, obj){alert("There was an error")},
				type: "POST"
			});
}

function GetSubforumsList(){
	var response = callService("GetSubforumsList", "", function(result){
		var sfList = $('#subforumsTable');
		$.each(result, function(i)
			{
				var tr = document.createElement("tr");
				var td = document.createElement("td");
				td.innerHTML = result[i];
				tr.appendChild(td);
				td.setAttribute('onclick', 'GetSubforum(\'' + result[i] + '\')');
				sfList.append(tr);
			}
		)
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

function GetSubforum(name)
{
	callService("GetSubforum", {"subforum": name},
		function(result)
		{
			alert(JSON.stringify(result));
		}
	);
}