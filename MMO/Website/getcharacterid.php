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

            //Protect from MySQL injections

            $charactername = stripslashes($charactername);
            $charactername = ucfirst($charactername);
            $charactername = mysqli_real_escape_string($con, $charactername);
            //Check username and password

                    $sql = "SELECT id, charactername
                        FROM characters
                        WHERE charactername = '$charactername'";

                $result = mysqli_query($con,$sql);
                $row = mysqli_fetch_array($result, MYSQLI_ASSOC);

                if(mysqli_num_rows($result) == 0) {

                    die ("Something went wrong.");

                } elseif(mysqli_num_rows($result) == 1) {
                        $result = mysqli_query($con, $sql);

                    echo "Character_ID" . $row['id'] . ";";
                }
?>