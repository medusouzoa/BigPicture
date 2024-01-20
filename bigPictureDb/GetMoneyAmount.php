<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "bigpictureinventory";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$id = isset($_POST['id']) ? $_POST['id'] : '';


$sql = "SELECT amount FROM moneydata WHERE id = $id";
$result = $conn->query($sql);

if ($result) {
    if ($result->num_rows > 0) {
        $row = $result->fetch_assoc();
        $amount = $row['amount'];
        echo "Amount: " . $amount;
    } else {
        echo "No records found.";
    }
} else {
    echo "Error retrieving amount: " . $conn->error;
}

$conn->close();
?>