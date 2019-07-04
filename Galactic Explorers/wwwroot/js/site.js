// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let barBase = new ProgressBar.Circle("#bar-base", {
    color: '#6f6',
    trailColor: '#0000',
    strokeWidth: 6,
    duration: 500,
    easing: 'easeOut'
});

let barBaseRed = new ProgressBar.Circle("#bar-red", {
    color: '#f33',
    trailColor: '#222',
    strokeWidth: 6,
    duration: 500,
    easing: 'easeOut'
});

let barShieldBase = new ProgressBar.Circle("#bar-shield-base", {
    color: '#66f',
    trailColor: '#0000',
    strokeWidth: 4.5,
    duration: 500,
    easing: 'easeOut'
});

let barShieldRed = new ProgressBar.Circle("#bar-shield-red", {
    color: '#f33',
    trailColor: '#222',
    strokeWidth: 4.5,
    duration: 500,
    easing: 'easeOut'
});

$(document).ready(function () {

    barBase.set(0.5);
    barBaseRed.set(0.5);
    barShieldBase.set(0.5);
    barShieldRed.set(0.5);

    setTimeout(function () {
        healthDamage(0.2);
        shieldDamage(0.5);
    }, 2000);

    setTimeout(function () {
        shieldHeal(1.0)
    }, 4000);

    setTimeout(function () {
        shieldDamage(0.7);
        healthDamage(0.1);
    }, 6000);
});

function healthDamage(damage) {
    barBase.animate(barBase.value() - damage);
    setTimeout(function () {
        barBaseRed.animate(barBaseRed.value() - damage);
    }, 400);
}

function shieldDamage(damage) {
    barShieldBase.animate(barShieldBase.value() - damage);
    setTimeout(function () {
        barShieldRed.animate(barShieldRed.value() - damage);
    }, 400);
}

function healthHeal(heal) {
    barBase.animate(barBase.value() + heal);
    barBaseRed.animate(barBaseRed.value() + heal);
}

function shieldHeal(heal) {
    barShieldBase.animate(barShieldBase.value() + heal);
    barShieldRed.animate(barShieldRed.value() + heal);
}