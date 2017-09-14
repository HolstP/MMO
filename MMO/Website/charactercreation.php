<?php

	// Database Information

	$server = "localhost";

	$user = "varygame_holst";

	$pass = "MHP19961130Nanna";

	$dbname= "varygame_SpaceMMO";


	// Create Connection

    $con = mysqli_connect($server, $user, $pass, $dbname);

    $con->set_charset('utf8mb4');


    // Check Connection

    if ($con->connect_error) {
   		die("Connection failed: " . $con->connect_error);
	}
	
	$charactername = $_POST['charactername'];
	$user_id = $_POST['user_id'];

	if ($charactername == "") {
		die ("Charactername can't be empty");
	}
	
	$sql = "SELECT charactername
			FROM characters
			WHERE charactername = '" . $charactername . "'";
	$result = mysqli_query($con, $sql);
	$row = mysqli_fetch_array($result, MYSQLI_ASSOC);
		if(mysqli_num_rows($result) > 0) {

			die ("Charactername already exists.");
				
			
		} else {
			
			
			$sql1 = "INSERT INTO characters
                     (charactername, user_id)
                     VALUES ('".$charactername."', '".$user_id."')";
                     $result1 = mysqli_query($con, $sql1);
			die ("Character created.");
			
		}

?>