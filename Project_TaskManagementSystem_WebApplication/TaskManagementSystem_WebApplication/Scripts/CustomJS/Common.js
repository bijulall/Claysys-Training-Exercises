function toggleDropdown() {
    var menu = document.getElementById('dropdownMenu');
    if (menu.style.display === 'block') {
        menu.style.display = 'none';
    } else {
        menu.style.display = 'block';
    }
}

// Event listener for profile picture click
document.getElementById('profilePic')?.addEventListener('click', function (event) {
    toggleDropdown();
    event.stopPropagation();  
});

// Event listener for "No profile photo available" click
document.getElementById('noPhoto')?.addEventListener('click', function (event) {
    toggleDropdown();
    event.stopPropagation(); 
});

// Close dropdown if user clicks outside
window.addEventListener('click', function () {
    document.getElementById('dropdownMenu').style.display = 'none';
});