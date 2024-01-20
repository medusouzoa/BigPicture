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

// Check if both itemName and newAmount are provided
if (empty($name) || empty($amount)) {
    die("Item name and new amount are required.");
}

$sql = "UPDATE inventoryitems
        SET amount = $amount
        WHERE EXISTS (
            SELECT 1
            FROM itemtype
            WHERE inventoryitems.itemId = itemtype.id
              AND itemtype.name = '$name'
        )";

if ($conn->query($sql) === TRUE) {
    echo "Amount updated successfully.";
} else {
    echo "Error updating amount: " . $conn->error;
}

$conn->close();
?>