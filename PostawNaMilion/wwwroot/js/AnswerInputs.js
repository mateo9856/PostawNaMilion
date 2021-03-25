let moneyVal = parseInt(document.getElementById('cashVal').innerText.match(/\d+/)[0], 10);
let betVal;
let moneyBet = 0;
let values = [0, 0, 0, 0];
function changeVal(e) {
    moneyVal = parseInt(document.getElementById('cashVal').innerText.match(/\d+/)[0], 10);
    const index = inputs.indexOf(this);
    values[index] = parseInt(e.target.value, 10);
    moneyBet = reduceArray();
    moneyVal -= moneyBet;
    if (moneyVal < 0) {
        betVal = document.getElementById('betCash').innerHTML = 0 + "<h6 class='text-danger'>Przekroczona wartość!</h6>";
    } else {
        betVal = document.getElementById('betCash').textContent = moneyVal;
    }
}

function reduceArray() {
    return moneyBet = values.reduce((a, b) => {
        return a + b;
    })
}

const inputs = [...document.querySelectorAll('input[type="number"]')];
console.log(inputs);
inputs.forEach((el) => {
    el.addEventListener("change", changeVal);
})
