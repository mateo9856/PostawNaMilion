const player = document.querySelector('.buttonStart');

const addPlayer = () => {
    $(".form").toggleClass("playerForm");
}

player.addEventListener("click", addPlayer);
