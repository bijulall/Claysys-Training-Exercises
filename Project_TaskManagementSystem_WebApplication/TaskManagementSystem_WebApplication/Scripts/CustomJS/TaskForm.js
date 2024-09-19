function setError(errorSpan, message) {
    let submibutton = document.getElementById("submitButton")
    errorSpan.className = 'errorSpan';
    errorSpan.innerText = message;
    submibutton.disabled = true;
}

function setSuccess(errorSpan) {
    let submibutton = document.getElementById("submitButton")
    errorSpan.className = 'successSpan';
    errorSpan.innerText = "";
    submibutton.disabled = false;
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
//validate form,
function validateTaskForm() {
    const nameInput = document.getElementById('name');
    const descriptionInput = document.getElementById('description');
    const createdDateInput = document.getElementById('CreatedDate');
    const dueDateInput = document.getElementById('DueDate');
    const assignedToInput = document.getElementById('AssignedTo');
    const projectInput = document.getElementById('ProjectID');
    const clientInput = document.getElementById('ClientID');

    if (nameInput.value.trim() === '' ||
        descriptionInput.value.trim() === '' ||
        createdDateInput.value.trim() === '' ||
        dueDateInput.value.trim() === '' ||
        assignedToInput.value === '' ||
        projectInput.value === '' ||
        clientInput.value === '') {
        alert("Please fill all the required fields.");
        return false;
    }
    alert("Form submitted successfully!");
    return true;
}
//validate client form
function validateClientForm() {
    const nameInput = document.getElementById('name');
    const mailInput = document.getElementById('mail');
    const descriptionInput = document.getElementById('description');

    if (nameInput.value.trim() === '' ||
        mailInput.value.trim() === '' ||
        descriptionInput.value.trim() === '') {
        alert("Please fill all the required fields.");
        return false;
    }
    alert("Form submitted successfully!");
    return true;
}
//validate report form
function validateReportForm() {
    const taskInput = document.querySelector('[name="TaskID"]');
    const nameInput = document.getElementById('name');
    const dateInput = document.getElementById('GeneratedOn');
    const descriptionInput = document.getElementById('description');

    if (taskInput.value === '' ||
        nameInput.value.trim() === '' ||
        dateInput.value.trim() === '' ||
        descriptionInput.value.trim() === '') {
        alert("Please fill all the required fields.");
        return false;
    }
    alert("Form submitted successfully!");
    return true;
}
//project form validation
function validateProjectForm() {
    const nameInput = document.getElementById('name');
    const clientInput = document.querySelector('[name="ClientID"]');
    const startDateInput = document.getElementById('StartDate');
    const endDateInput = document.getElementById('EndDate');
    const descriptionInput = document.getElementById('description');

    if (nameInput.value.trim() === '' ||
        clientInput.value === '' ||
        startDateInput.value.trim() === '' ||
        endDateInput.value.trim() === '' ||
        descriptionInput.value.trim() === '') {
        alert("Please fill all the required fields.");
        return false;
    }
    alert("Form submitted successfully!");
    return true;
}

//pasword reset form validation
function validatePasswordResetForm() {
    const emailInput = document.querySelector('[name="EmailAddress"]');
    const passwordInput = document.querySelector('[name="Password"]');
    const newPasswordInput = document.querySelector('[name="NewPassword"]');
    const confirmPasswordInput = document.querySelector('[name="ConfirmPassword"]');

    if (emailInput.value.trim() === '' ||
        passwordInput.value.trim() === '' ||
        newPasswordInput.value.trim() === '' ||
        confirmPasswordInput.value.trim() === '') {
        alert("Please fill all the required fields.");
        return false;
    }

    if (newPasswordInput.value !== confirmPasswordInput.value) {
        alert("New password and confirmation do not match.");
        return false;
    }

    alert("Form submitted successfully!");
    return true;
}
//validate password
function validateUserPassword() {
    const passwordInput = document.getElementById('userPassword');
    const errorSpan = document.getElementById('userPasswordError');

    // Regular expression to check for at least 5 characters, one symbol, and one number
    const passwordRegex = /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{6,}$/;

    if (passwordInput.value.trim() === '') {
        setError(errorSpan, "Password is required");
    }
    else if (!passwordRegex.test(passwordInput.value.trim())) {
        setError(errorSpan, "Password must be at least 6 characters long,must contain one lowercase and uppercase letter and at least one symbol and one number.");
    } else {
        setSuccess(errorSpan);
    }
}