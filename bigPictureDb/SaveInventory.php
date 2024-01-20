<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "bigpictureinventory";
$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
// Retrieve values from the POST request
$name = isset($_POST['name']) ? $_POST['name'] : '';
$amount = isset($_POST['amount']) ? $_POST['amount'] : '';
echo "Connected Successfully";
$sql = "INSERT INTO inventory (name, amount) VALUES ('" . $name . "' ,'" . $amount . "')";

$result = $conn->query($sql);

echo "Item added successfully.";
$conn->close();

?>