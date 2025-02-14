

// Puedes agregar interactividad adicional aquí si es necesario- --- NAVBAR
document.addEventListener('DOMContentLoaded', function () {

    const links = document.querySelectorAll('.navbar a');
    links.forEach(link => {
        link.addEventListener('click', function () {
            links.forEach(l => l.classList.remove('active'));
            this.classList.add('active');
        });
    });
});


//CARRUSEL DE IMAGENES DEL HOME PRINCIPAL  












// calidad yt dedesmpeño  
window.addEventListener('load', () => {
    const animatedImage = document.querySelector('.animated-image');


    setTimeout(() => {
        animatedImage.classList.add('visible');
    }, 2000);
});









//////// Pcarrusel promociones jarro   //////
document.addEventListener("DOMContentLoaded", function () {
    const slides = document.querySelectorAll(".carousel-slide");
    const prevBtn = document.querySelector(".prev");
    const nextBtn = document.querySelector(".next");

    let currentIndex = 0;

    function showSlide(index) {
        slides.forEach((slide, i) => {
            slide.classList.toggle("active", i === index);
        });
    }

    function nextSlide() {
        currentIndex = (currentIndex + 1) % slides.length;
        showSlide(currentIndex);
    }

    function prevSlide() {
        currentIndex = (currentIndex - 1 + slides.length) % slides.length;
        showSlide(currentIndex);
    }

    nextBtn.addEventListener("click", nextSlide);
    prevBtn.addEventListener("click", prevSlide);

    // Cambio automático cada 5 segundos
    setInterval(nextSlide, 5000);
});







//////// PAGINAS DE MOTOS  //////


// Animación para las tarjetas
const cards = document.querySelectorAll('.card');
cards.forEach(card => {
    card.addEventListener('mouseenter', () => {
        card.style.transform = 'scale(1.05)';
    });
    card.addEventListener('mouseleave', () => {
        card.style.transform = 'scale(1)';
    });
});


//////// cotizacion .html  //////

document.addEventListener("DOMContentLoaded", function () {
    emailjs.init("TU_USER_ID");  // Reemplaza con tu User ID de EmailJS
});

function enviarCotizacion() {
    let nombre = document.getElementById("nombre").value;
    let email = document.getElementById("email").value;
    let telefono = document.getElementById("telefono").value;
    let moto = document.getElementById("moto").value;
    let mensaje = document.getElementById("mensaje").value;

    let accesorios = [];
    document.querySelectorAll('input[name="accesorios"]:checked').forEach((item) => {
        accesorios.push(item.value);
    });

    let data = {
        nombre: nombre,
        email: email,
        telefono: telefono,
        moto: moto,
        accesorios: accesorios.join(", "),
        mensaje: mensaje
    };

    emailjs.send("TU_SERVICE_ID", "TU_TEMPLATE_ID", data)
        .then(function (response) {
            alert("Cotización enviada correctamente.");
        }, function (error) {
            alert("Error al enviar la cotización. Intenta de nuevo.");
        });
}

function enviarWhatsApp() {
    let nombre = document.getElementById("nombre").value;
    let telefono = document.getElementById("telefono").value;
    let moto = document.getElementById("moto").value;
    let mensaje = document.getElementById("mensaje").value;

    let accesorios = [];
    document.querySelectorAll('input[name="accesorios"]:checked').forEach((item) => {
        accesorios.push(item.value);
    });

    let empresaTelefono = "51987654321";  // Reemplaza con el número de la empresa
    let texto = `🚀 *Nueva Cotización* 🚀%0A
  👤 Nombre: ${nombre}%0A
  📞 Teléfono: ${telefono}%0A
  🏍 Moto: ${moto}%0A
  🛠 Accesorios: ${accesorios.join(", ")}%0A
  📝 Mensaje: ${mensaje}`;

    let url = `https://wa.me/${empresaTelefono}?text=${texto}`;
    window.open(url, "_blank");
}



