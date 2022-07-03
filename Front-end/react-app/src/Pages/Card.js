import React, { Component } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Variables } from "./Components/Variables";

class CardClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      title: "",
      text: "",
    };
    this.deleteCard = this.deleteCard.bind(this);
  }

  refreshCard() {
    fetch(Variables.API_URL + "/Card/" + this.props.cardId, {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    })
      .catch((error) => {
        alert("Something go wrong, please try again later.");
      })
      .then((value) => value.json())
      .then((result) => {
        this.setState({
          title: result.title,
          text: result.text,
        });
      });
  }

  deleteCard() {
    
    fetch(Variables.API_URL + "/Card/" + this.props.cardId, {
      method: "DELETE",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    }).then((result) => {
      console.log(result);
      alert("Card deleted");
      this.props.navigation("/");
    });
  }
  componentDidMount() {
    this.refreshCard();
  }

  render() {
    const { title, text} = this.state;
    return (
      <article>
        <div className="cardPage">
          <h1>{title}</h1>
          <p>{text}</p>
        </div>
        <div className="cardPageButtons">
          <button className="cardButton" onClick={this.deleteCard}>
            delete
          </button>
          <button
            className="cardButton"
            onClick={() =>
              this.props.navigation("/cardChange/" + this.props.cardId)
            }
          >
            change
          </button>
        </div>
      </article>
    );
  }
}

// Wrap and export
export default function Card(props) {
  let { id } = useParams();
  const navigation = useNavigate();

  return <CardClass {...props} cardId={id} navigation={navigation} />;
}
