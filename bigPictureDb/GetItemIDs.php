<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "bigpictureinventory";
$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected Successfully";
$sql = "SELECT id FROM inventory";
$result = $conn->query($sql);
$rows = array();
if ($result->num_rows > 0) {
    while ($row = $result->fetch_assoc()) {
        $rows[] = $row;
    }
    echo json_encode($rows);
} else {
    echo "0 results";
}
$conn->close();


?>