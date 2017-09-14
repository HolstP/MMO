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

            $email = $_POST['email'];

            //Protect from MySQL injections

            $email = stripslashes($email);
            $email = ucfirst($email);
            $email = mysqli_real_escape_string($con, $email);
            //Check username and password

                    $sql = "SELECT id, user_email
                        FROM users
                        WHERE user_email = '$email'";

                $result = mysqli_query($con,$sql);
                $row = mysqli_fetch_array($result, MYSQLI_ASSOC);

                if(mysqli_num_rows($result) == 0) {

                    die ("Something went wrong.");

                } elseif(mysqli_num_rows($result) == 1) {
                        $result = mysqli_query($con, $sql);

                    echo "User_ID" . $row['id'] . ";";
                }
?>