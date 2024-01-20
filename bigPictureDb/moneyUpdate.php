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
$amount = isset($_POST['amount']) ? $_POST['amount'] : '';

$sql = "UPDATE moneydata
        SET amount = $amount
        WHERE id = $id";

if ($conn->query($sql) === TRUE) {
    echo "Amount updated successfully.";
} else {
    echo "Error updating amount: " . $conn->error;
}

$conn->close();
?>