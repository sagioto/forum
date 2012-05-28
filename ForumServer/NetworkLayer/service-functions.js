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
	var response = callService("GetSubforumsList", "", function(result){alert(result);});
}

function RegisterUser(user){
	var methodName = "Register";
	var response = callService(methodName, user,
		function(result){
		alert("Result is: " + result[methodName + "Result"]);
		}
	);
}

function Register(){
	var name = $('input[name="username"]').val();
	var pass = $('input[name="password"]').val();
	return RegisterUser({"username": name, "password": pass});
}