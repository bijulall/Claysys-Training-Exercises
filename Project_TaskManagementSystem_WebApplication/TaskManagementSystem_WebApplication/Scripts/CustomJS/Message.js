//set error
function setError(errorSpan, message) {
    let submitbutton = document.getElementById("submitButton")
    errorSpan.className = 'errorSpan';
    errorSpan.innerText = message;
    submitbutton.disabled = true;
}
// set success 
function setSuccess(errorSpan) {
    let submitbutton = document.getElementById("submitButton")
    errorSpan.className = 'successSpan';
    errorSpan.innerText = "";
    submitbutton.disabled = false;
}

function validateName() {
    const firstnameInput = document.getElementById('name');
    const errorSpan = document.getElementById('nameError');

    const alphaRegex = /^[A-Za-z\s]+$/;
    if (firstnameInput.value.trim() === '') {
        setError(errorSpan, "This field is required");
    }
    else if (!alphaRegex.test(firstnameInput.value.trim())) {
        setError(errorSpan, "Name can only contain letters and spaces.");

    } else {
        setSuccess(errorSpan);

    }
}
function validateSubject() {
    const descriptionInput = document.getElementById('subject');
    const errorSpan = document.getElementById('SubjectError');
    if (descriptionInput.value.trim() === '') {
        setError(errorSpan, "This field is required");
    } else {
        setSuccess(errorSpan);

    }
}
//validate message
function validateDescription() {
    const descriptionInput = document.getElementById('description');
    const errorSpan = document.getElementById('DescriptionError');
    if (descriptionInput.value.trim() === '') {
        setError(errorSpan, "This field is required");
    } else {
        setSuccess(errorSpan);

    }
}
function validateMail() {
    const passportNoInput = document.getElementById('mail'); // Use for email input
    const errorSpan = document.getElementById('mailError'); // Error span remains the same
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Regular expression for email validation

    if (emailRegex.test(passportNoInput.value.trim())) {
        setSuccess(errorSpan);
    }
    else {
        setError(errorSpan, 'Please enter a valid email address.');
    }
}

function validateContactForm() {
    const nameInput = document.getElementById('name');
    const emailInput = document.getElementById('mail');
  
    const subjectInput = document.getElementById('subject');
    const messageInput = document.getElementById('description');
    if (nameInput.value.trim() === '' || emailInput.value.trim() === '' ||
        subjectInput.value.trim() === '' || messageInput.value.trim() === '')
        {
            alert("Please fill all the required fields.");
            return false;
    }
    alert("Form submitted successfully!");
    return true;
}