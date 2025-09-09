// Define an asynchronous function to fetch the data
async function getEmployeeData() {
  // The URL of the JSON endpoint
  const apiUrl = 'https://dummy.restapiexample.com/api/v1/employees';

  try {
    // Await the fetch call to get a Response object
    const response = await fetch(apiUrl);

    // Check if the response was successful (status code 200-299)
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    // Await the parsing of the response body as JSON
    const data = await response.json();

    // Display the retrieved data on the console
    console.log("Successfully retrieved data:");
    console.log(data);

  } catch (error) {
    // Catch and log any errors that occur during the fetch operation
    console.error("Could not fetch data:", error);
  }
}

// Call the function to execute the request
getEmployeeData();