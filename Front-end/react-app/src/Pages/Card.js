import React, { Component } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Variables } from "./Components/Variables";

class CardClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      title: "",
      text: "",
      date: "",
      userId: 0,
      likes: 0,
      disLikes: 0,
    };
    this.deleteCard = this.deleteCard.bind(this);
    this.likeCard = this.likeCard.bind(this);
    this.disLikeCard = this.disLikeCard.bind(this);
    this.refreshCard = this.refreshCard.bind(this);
  }

  likeCard() {
    let answerOk = false;
    fetch(Variables.API_URL + "/Card/like/" + this.props.cardId, {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    })
      .then((res) => {
        if (res.status === 200) {
          answerOk = true;
          return;
        } else if (res.status === 401) {
          alert("You was unauthorized please login again.");
          this.props.navigation("/login");
          window.location.reload(false);
          return;
        } else if (res.status === 404) {
          alert("Card dont existed");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
      .then((result) => {
        if (!answerOk) {
          return;
        }
        this.refreshCard();
      });
  }
  disLikeCard() {
    let answerOk = false;
    fetch(Variables.API_URL + "/Card/dislike/" + this.props.cardId, {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    })
      .then((res) => {
        if (res.status === 200) {
          answerOk = true;
          return;
        } else if (res.status === 401) {
          alert("You was unauthorized please login again.");
          this.props.navigation("/login");
          window.location.reload(false);
          return;
        } else if (res.status === 404) {
          alert("Card dont existed");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
      .then((result) => {
        if (!answerOk) {
          return;
        }
        this.refreshCard();
      });
  }
  deleteCard() {
    let answerOk = false;
    fetch(Variables.API_URL + "/Card/" + this.props.cardId, {
      method: "DELETE",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    })
      .then((res) => {
        if (res.status === 200) {
          answerOk = true;
          return;
        } else if (res.status === 401) {
          alert("You was unauthorized please login again.");
          this.props.navigation("/login");
          window.location.reload(false);
          return;
        } else if (res.status === 403) {
          alert("You cant delete this card, not enough rights");
          return;
        } else if (res.status === 404) {
          alert("Card dont existed");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
      .then((result) => {
        if (!answerOk) {
          return;
        }
        alert("Card deleted");
        this.props.navigation("/");
      });
  }

  refreshCard() {
    let answerOk = false;
    fetch(Variables.API_URL + "/Card/" + this.props.cardId, {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    })
      .then((res) => {
        if (res.status === 200) {
          answerOk = true;
          return res.json();
        } else if (res.status === 401) {
          alert("You was unauthorized please login again.");
          this.props.navigation("/login");
          window.location.reload(false);
          return;
        } else if (res.status === 404) {
          alert("Card not found");
          this.props.navigation("/cards");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
      .then((result) => {
        if (!answerOk) {
          return;
        }
        this.setState({
          title: result.title,
          text: result.text,
          date: result.creationDate.slice(0, 10),
          likes: result.likes,
          userId: result.userId,
          disLikes: result.disLikes,
        });
      });
  }

  componentDidMount() {
    this.refreshCard();
  }

  render() {
    const { title, text, date, likes, disLikes, userId } = this.state;
    let likefunction, dislikefunction, deleteCardFunction, changeCardFunction;
    if (sessionStorage.getItem("Id") !== null) {
      likefunction = this.likeCard;
      dislikefunction = this.disLikeCard;
      deleteCardFunction = this.deleteCard;
      changeCardFunction = () =>
        this.props.navigation("/cardChange/" + this.props.cardId);
    } else {
      let alertFunction = () => alert("You are not registered");
      likefunction = alertFunction;
      dislikefunction = alertFunction;
      deleteCardFunction = alertFunction;
      changeCardFunction = alertFunction;
    }
    return (
      <article>
        <div className="cardPage">
          <h1>{title}</h1>
          <p>{text}</p>
          <p>{date}</p>
          <p>
            <i className="fa-solid fa-thumbs-up" onClick={likefunction}></i>{" "}
            {likes} {" | "}
            <i
              className="fa-solid fa-thumbs-down"
              onClick={dislikefunction}
            ></i>{" "}
            {disLikes}
          </p>
        </div>
        <div className="cardPageButtons">
          <button className="cardButton" onClick={deleteCardFunction}>
            delete
          </button>
          <button className="cardButton" onClick={changeCardFunction}>
            change
          </button>
        </div>
      </article>
    );
  }
}
export default function Card(props) {
  let { id } = useParams();
  const navigation = useNavigate();

  return <CardClass {...props} cardId={id} navigation={navigation} />;
}
