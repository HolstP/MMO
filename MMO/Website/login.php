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

            $currentVersion = "Dev 0.0.2.0";
            $email = $_POST['email'];
            $password = $_POST['password'];
            $clientVersion = $_POST['clientVersion'];

            //Protect from MySQL injections

            $email = stripslashes($email);
            $email = ucfirst($email);
            $password = stripslashes($password);
            $password = md5($password);
            $email = mysqli_real_escape_string($con, $email);
            $password = mysqli_real_escape_string($con, $password);
            //Check Email and password

            if ($email == "") {
                die ("Please fill out Email.");
            }

            if ($password == "") {
                die ("Please fill out Password.");
            }

            if ($currentVersion != $clientVersion) {

                die ("Outdated");

            } else if ($currentVersion == $clientVersion) {

                    $sql = "SELECT user_email, user_password
                        FROM users
                        WHERE user_email = '$email' AND user_password = '$password'";

                $result = mysqli_query($con,$sql);
                $row = mysqli_fetch_array($result, MYSQLI_ASSOC);

                if(mysqli_num_rows($result) == 0) {

                    die ("Incorrect Email or Password.");

                } elseif(mysqli_num_rows($result) == 1) {
                        $result = mysqli_query($con, $sql);
                    die ("Log in successful!");
                }

            }
?>