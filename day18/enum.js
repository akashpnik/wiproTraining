var data = 42;
data = "Now I'm a string!";
data = { key: 'value' };
data = [1, 2, 3, 4, 5];
console.log(data); // Output: { key: 'value' }
data = { name: 'Alice', age: 30 };
console.log(data); // Output: { name: 'Alice', age: 30 }
var Status;
(function (Status) {
    Status["Pending"] = "PENDING";
    Status["InProgress"] = "IN_PROGRESS";
    Status["Complete"] = "COMPLETE";
})(Status || (Status = {}));
console.log(Status.Pending); // Output: PENDING
console.log(Status.InProgress); // Output: IN_PROGRESS
console.log(Status.Complete); // Output: COMPLETE
console.log(Status[0]); // Output: PENDING
