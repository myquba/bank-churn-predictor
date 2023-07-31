function getFormData() {
  var form = document.getElementById("churnForm");
  var data = {};
  for (var i = 0; i < form.elements.length; i++) {
    var e = form.elements[i];
    if (e.name) {
      data[e.name] = e.value;
    }
  }
  return data;
}

function sendDataToServer(event) {
  event.preventDefault();  // Prevent the form from being submitted normally
  var data = getFormData();
  
  fetch('http://localhost:5000/predict', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  })
  .then(response => response.json())
  .then(data => {
    console.log('Success:', data);
  })
  .catch((error) => {
    console.error('Error:', error);
  });
}

// Add an event listener for the form's submit event
document.getElementById("churnForm").addEventListener('submit', sendDataToServer);
