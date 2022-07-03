import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate } from "react-router-dom";

class CardCreateClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      title: "",
      text: "",
      categoryName: "",
    };

    this.changeTitle = this.changeTitle.bind(this);
    this.changeText = this.changeText.bind(this);
    this.changeCategoryName = this.changeCategoryName.bind(this);
    this.createNewCard = this.createNewCard.bind(this);
  }
  changeTitle(event) {
    this.setState({ title: event.target.value });
  }
  changeText(event) {
    this.setState({ text: event.target.value });
  }
  changeCategoryName(event) {
    this.setState({ categoryName: event.target.value });
  }

  createNewCard() {
    fetch(Variables.API_URL + "/Card/Create", {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
      body: JSON.stringify({
        title: this.state.title,
        text: this.state.text,
        categoryName: this.state.categoryName,
      }),
    })
      .catch((error) => {
        alert("Something go wrong, please try again later.");
      })
      .then((res) => res.json())
      .then((result) => {
        console.log(result);
      });
  }
  render() {
    const { navigation } = this.props;
    const { title, text, categoryName } = this.state;
    return (
      <article>
        <div className="loginForm">
          <div className="inputDiv">
            <span>Title:</span>
            <input
              className="input"
              type="text"
              value={title}
              onChange={this.changeTitle}
              placeholder="Card Title"
            ></input>
          </div>
          <div className="inputDiv">
            <span>Text:</span>
            <textarea
              className="inputTextArea"
              type="text"
              value={text}
              onChange={this.changeText}
              placeholder="Card text"
            ></textarea>
          </div>
          <div className="inputDiv">
            <span>Category Name:</span>
            <input
              className="input"
              type="text"
              value={categoryName}
              onChange={this.changeCategoryName}
              placeholder="Category Name"
            ></input>
          </div>
          <button className="simpleButton" onClick={this.createNewCard}>
            Create
          </button>
        </div>
      </article>
    );
  }
}

export default function CardCreate(props) {
  const navigation = useNavigate();

  return <CardCreateClass {...props} navigation={navigation} />;
}
