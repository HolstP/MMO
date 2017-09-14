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
	
	$user_id = $_POST['user_id'];
	$charactername = $_POST['charactername'];
	
	$sql = "DELETE
			FROM characters
			WHERE user_id = '" . $user_id . "' AND charactername = '" . $charactername . "'";
	$result = mysqli_query($con, $sql);
	$row = mysqli_fetch_array($result, MYSQLI_ASSOC);
		if(mysqli_num_rows($result) > 0) {
			
			die ("Character Deleted.");
			
		} else {
			
			die ("No character found.");
			
		}
?>