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
	
	$character_id = $_POST['character_id'];
	
	//$userIP = $_SERVER['REMOTE_ADDR'];
	
	$sql = "SELECT id, posx, posy, posz, level, xp
			FROM characters
			WHERE id = '" . $character_id . "'";
	$result = mysqli_query($con, $sql);
		if(mysqli_num_rows($result) > 0) {
			
			while($row = mysqli_fetch_assoc($result)) {
				
				echo "posX " . $row['posx'] . "|posY " . $row['posy'] . "|posZ " . $row['posz'] . "|level " . $row['level'] ." |xp " . $row['xp'] .";";
				
			}
			
		} else {
			
			die ("No character found.");
			
		}
?>