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
	$posX = $_POST['posX'];
	$posY = $_POST['posY'];
	$posZ = $_POST['posZ'];
	$level = $_POST['level'];
	$xp = $_POST['xp'];

	if ($charactername == "") {
		die("Whoops");
	}
	
	$sql = "UPDATE characters 
			SET posx = $posX, posy = $posY, posz = $posZ, level = $level, xp = $xp
			WHERE charactername = '" . $charactername . "'";
	$result = mysqli_query($con, $sql);

?>