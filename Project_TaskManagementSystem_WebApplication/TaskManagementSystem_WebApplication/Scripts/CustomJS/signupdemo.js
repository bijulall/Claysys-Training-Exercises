//drop down for states and cities
$(document).ready(function () {
    $('#stateDropdown').change(function () {
        var stateId = $(this).val(); 
        var cityDropdown = $('#cityDropdown'); 
        if (stateId) {          
            cityDropdown.empty().append('<option value="">Loading cities...</option>');
            // AJAX request to fetch cities based on selected state
            $.ajax({
                url: '/HomePage/GetCities', 
                type: 'GET',
                data: { stateId: stateId }, 
                success: function (cities) {
                    cityDropdown.empty();
                    cityDropdown.append('<option value="">Select City</option>');
                    // Loop through returned cities and append them to the dropdown
                    $.each(cities, function (index, city) {
                        cityDropdown.append('<option value="' + city.Value + '">' + city.Text + '</option>');
                    });
                },
                error: function () {
                    alert('Error loading cities');
                    cityDropdown.empty().append('<option value="">Select City</option>');
                }
            });
        } else {
            cityDropdown.empty().append('<option value="">Select City</option>');
        }
    });
});
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
// validate firstname
function validateFirstname() {
    const firstnameInput = document.getElementById('firstname');
    const errorSpan = document.getElementById('firstnameError');

    const alphaRegex = /^[A-Za-z\s]+$/;
    if (firstnameInput.value.trim() === '') {
        setError(errorSpan, "First name is required");
    }
    else if (!alphaRegex.test(firstnameInput.value.trim())) {
        setError(errorSpan, "First name can only contain letters and spaces.");

    } else {
        setSuccess(errorSpan);
    }
}
// validate secondname
function validateLastname() {
    const lastnameInput = document.getElementById('lastname');
    const errorSpan = document.getElementById('lastnameError');
    const alphaRegex = /^[A-Za-z\s]+$/;
    if (lastnameInput.value.trim() === '') {
        setError(errorSpan, "Last name is required");
        return false
    }
    else if (!alphaRegex.test(lastnameInput.value.trim())) {
        setError(errorSpan, "Last name can only contain letters and spaces.");
        return false
    } else {
        setSuccess(errorSpan);
        return true
    }
}
//validate phone
function validatePhone() {
    const phoneNoInput = document.getElementById('phone');
    const errorSpan = document.getElementById('phoneError'); 
    const phoneRegex = /^[0-9]{10}$/;
    if (phoneRegex.test(phoneNoInput.value.trim())) {
        setSuccess(errorSpan);
    }
    else {
        setError(errorSpan, 'Please enter a valid 10-digit phone number.');
    }
}
//validate mail
function validateEmail() {
    const passportNoInput = document.getElementById('email'); 
    const errorSpan = document.getElementById('emailError'); 
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; 

    if (emailRegex.test(passportNoInput.value.trim())) {
        setSuccess(errorSpan);
    }
    else {
        setError(errorSpan, 'Please enter a valid email address.');
    }
}

//validate user name
function validateUsername() {
    const firstnameInput = document.getElementById('username');
    const errorSpan = document.getElementById('usernameError');

    const alphaRegex = /^[A-Za-z\s]+$/;
    if (firstnameInput.value.trim() === '') {
        setError(errorSpan, "User name is required");
    }
    else if (!alphaRegex.test(firstnameInput.value.trim())) {
        setError(errorSpan, "Username can only contain letters and spaces.");

    } else {
        setSuccess(errorSpan);
    }
}

//validate password
function validateUserPassword() {
    const passwordInput = document.getElementById('userPassword');
    const errorSpan = document.getElementById('userPasswordError');
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
//validate confirm password
function validateConfirmPassword() {
    const passwordInput = document.getElementById('userPassword'); 
    const confirmPasswordInput = document.getElementById('confirmPassword'); 
    const errorSpan = document.getElementById('confirmPasswordError'); 
    if (passwordInput.value.trim() === confirmPasswordInput.value.trim()) {
        setSuccess(errorSpan);
    } else {
        setError(errorSpan, 'Passwords do not match.');
    }
}
//validateform
function validateForm() {
    const firstnameInput = document.getElementById('firstname');
    const emailInput = document.getElementById('email');
    const phoneInput = document.getElementById('phone');
    const usernameInput = document.getElementById('username');
    const passwordInput = document.getElementById('password');
    const confirmPasswordInput = document.getElementById('confirmPassword');
    if (firstnameInput.value.trim() === '' || emailInput.value.trim() === '' ||
        phoneInput.value.trim() === '' || usernameInput.value.trim() === '' ||
        passwordInput.value.trim() === '' || confirmPasswordInput.value.trim() === '') {
        alert("Please fill all the required fields.");
        return false;
    }
    if (passwordInput.value !== confirmPasswordInput.value) {
        alert("Passwords do not match.");
        return false;
    }
    alert("Form submitted successfully!");
    return true;
}






