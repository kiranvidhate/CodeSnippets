<?php

	$result = selectValues();
	$rows = array();

	if (mysqli_num_rows($result) > 0) {
			while($row = mysqli_fetch_assoc($result)) {
				 $rows[] = $row;
				
			}
		}

	echo json_encode($rows);
	

	function connectDataBase(){
		$servername = "localhost";
		$username = "root";
		$password = "";
		
		/************CREATE CONNECTION********************/
		// Create connection
		$conn = mysqli_connect($servername, $username, $password);
		
		// Check connection
		if (!$conn) {
			die("Connection failed: " . mysqli_connect_error());
		}
		
		//echo "<br/>Connected successfully";
		return $conn;
	}

	function selectValues(){
		 $conn = connectDataBase();
		 mysqli_select_db( $conn, "employees");
		 
		/************SELECT VALUES********************/
		$sql = "SELECT name, dateOfBirth, gender, salary 
				FROM employees";

		$result = mysqli_query($conn, $sql);
		closeDataBase($conn);
	
		return $result;
	}

	function closeDataBase($conn){
		mysqli_close($conn);
	}

?>