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

// Check if itemName is provided
if (empty($name)) {
    die("Item name is required.");
}

$sql = "SELECT inventoryitems.amount
        FROM inventoryitems
        INNER JOIN itemtype ON inventoryitems.itemId = itemtype.id
        WHERE itemtype.name = '$name'";

$result = $conn->query($sql);

if ($result) {
    $row = $result->fetch_assoc();
    if ($row) {
        $amount = $row['amount'];
        echo "The amount for $name is: $amount";
    } else {
        echo "No records found for $name.";
    }
} else {
    echo "Error executing query: " . $conn->error;
}

$conn->close();
?>