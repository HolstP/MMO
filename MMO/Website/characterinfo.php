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

	//include ("lvlsystem.php");
	//"|Level" . $level .
	
	$user_id = $_POST['user_id'];
	
	//$userIP = $_SERVER['REMOTE_ADDR'];
	
	$sql = "SELECT user_id, charactername, level
			FROM characters
			WHERE user_id = '" . $user_id . "'
			ORDER BY id";
	$result = mysqli_query($con, $sql);
		if(mysqli_num_rows($result) > 0) {
			
			while($row = mysqli_fetch_assoc($result)) {
				
				echo "Charactername " . $row['charactername'] . "|Level " . $row['level'] .";";
				
			}/*

			$sql1 = "UPDATE characters
					SET ip = '$userIP'
					WHERE charactername = '$username'";
			$result1 = mysqli_query($con, $sql1);*/
			
		} else {
			
			die ("No character found.");
			
		}
?>