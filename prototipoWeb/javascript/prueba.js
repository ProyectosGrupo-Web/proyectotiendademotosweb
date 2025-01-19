window.addEventListener('load', () => {
  updateCarousel();
  setInterval(nextSlide, 3000);
});
// depuara
//para el carrusel 
const track = document.querySelector('.carousel-track');
const slides = Array.from(track.children);

let currentIndex = 0;
const totalSlides = slides.length;

function updateCarousel() {
  const slideWidth = slides[0].getBoundingClientRect().width;
  const offset = -currentIndex * slideWidth;
  track.style.transform = `translateX(${offset}px)`;
}

// Función para mover al siguiente slide
function nextSlide() {
  currentIndex = (currentIndex + 1) % totalSlides;
  updateCarousel();
}

// Mover al siguiente slide automáticamente cada 3 segundos
setInterval(nextSlide, 3000);

// Inicializa el carrusel moviendo a la primera posición
updateCarousel();



// para la galeria de motos 

// Cambiar color de fondo
function changeBackground() {
  const colors = ["#161917", "#70160E", "#9E0004", "#B9030F", "#E1E3DB"];
  const randomColor = colors[Math.floor(Math.random() * colors.length)];
  document.body.style.backgroundColor = randomColor;
}
