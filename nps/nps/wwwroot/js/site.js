

function selectNPS(value) {
    document.querySelectorAll('.smiley-btn').forEach(btn => {
        btn.classList.remove('active');
    });
    
    document.querySelector(`.smiley-btn[data-value="${value}"]`).classList.add('active');
    
    document.getElementById('selectedNPS').value = value;
    
    
}