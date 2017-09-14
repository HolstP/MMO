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
	
	$sql = "SELECT user_id, charactername
			FROM characters
			WHERE user_id = '" . $user_id . "'";
	if ($result = mysqli_query($con, $sql))
	{
		$rowcount = mysqli_num_rows($result);
		die("Player has " . $rowcount . " characters.");
	}
?>