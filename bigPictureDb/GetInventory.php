<?php
require 'ConnectionSettings.php';

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT itemtype.name, inventoryitems.amount
        FROM inventoryitems
        INNER JOIN itemtype ON inventoryitems.itemId = itemtype.id";

$result = $conn->query($sql);
$rows = array();

if ($result) {
    while ($row = $result->fetch_assoc()) {
        $rows[] = $row;
    }
    echo json_encode($rows);
} else {
    echo "Error executing query: " . $conn->error;
}

$conn->close();
?>