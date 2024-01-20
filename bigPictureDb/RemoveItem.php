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
$id = isset($_POST['id']) ? $_POST['id'] : '';

// Check if the ID is provided
if (empty($id)) {
    die("Item ID is required.");
}

$sql = "DELETE FROM inventory WHERE id = $id";

if ($conn->query($sql) === TRUE) {
    echo "Item removed successfully.";
} else {
    echo "Error removing item: " . $conn->error;
}

$conn->close();
?>
