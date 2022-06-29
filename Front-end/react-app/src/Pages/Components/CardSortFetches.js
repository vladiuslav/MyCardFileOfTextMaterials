import { Variables } from "./Variables";

export function GetCards() {

}

export function GetMostLikedCards() {
fetch(Variables.API_URL + "/Card/mostLikedCards", {
    method: "GET",
    headers: {
    accept: "application/json",
    "Content-Type": "application/json",
    Authorization: "Bearer " + sessionStorage.getItem("access_token"),
    },
})
    .then((res) => res.json())
    .then((value) => {
    return value;
    });
}

export function GetCardsByCategory(id) {
fetch(Variables.API_URL + "/GetCardsByCategory/" + id, {
    method: "GET",
    headers: {
    accept: "application/json",
    "Content-Type": "application/json",
    Authorization: "Bearer " + sessionStorage.getItem("access_token"),
    },
})
    .then((res) => res.json())
    .then((value) => {
    return value;
    });
}

