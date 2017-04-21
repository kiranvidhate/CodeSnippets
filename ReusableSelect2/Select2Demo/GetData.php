<?php
	$conn = "";
	
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
      
		return $conn;
	}
	

	function selectValues($search){
		 $conn = connectDataBase();
		 mysqli_select_db( $conn, "users");
		 
		/************SELECT VALUES********************/
		$sql = "SELECT user_id, user_fullname  
				FROM users
                WHERE user_fullname LIKE '%" . $search ."%'";  
      
		$result = mysqli_query($conn, $sql);
      
      $return = [];
      while($row = mysqli_fetch_assoc($result)) {
        $return[] = [ 
          'id' => $row['user_id'],
          'name' => $row['user_fullname']
        ];
      }
      
      header('Content-type: application/json');
      echo json_encode($return);
	}
?>
<?php 
   selectValues($_GET["q"]);
?>