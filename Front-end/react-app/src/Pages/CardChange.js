import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate, useParams } from "react-router-dom";

class CardChangeClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      title: "",
      text: "",
      categoryName: ""
    };

    this.changeTitle = this.changeTitle.bind(this);
    this.changeText = this.changeText.bind(this);
    this.changeCategoryName = this.changeCategoryName.bind(this);
    this.changeCard = this.changeCard.bind(this);
    this.refreshList = this.refreshList.bind(this);
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
  componentDidMount() {
    this.refreshList();
  }
  refreshList() {
    fetch(Variables.API_URL + "/Card/" + this.props.cardId, {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    })
      .then((res) => res.json())
      .then((value) => {
        this.setState({
          title: value.title,
          text: value.text,
        });
        fetch(Variables.API_URL + "/Category/" + value.categoryId, {
          method: "GET",
          headers: {
            accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + sessionStorage.getItem("access_token"),
          },
        })
        .then((res) => res.json())
        .then((value) => {
          this.setState({
            categoryName: value,
          });
        });
    });
  }
  

  changeCard() {
    fetch(Variables.API_URL + "/Card/" + this.props.cardId, {
      method: "PUT",
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
      .then((res) => res.json())
      .then((result) => {
        alert("Changed");
        this.props.navigation("/Card/" + this.props.cardId);
      });
  }
  render() {
    const { navigation } = this.props;
    const { title, text, categoryName } = this.state;
    return (
      <article>
        <div>
          <span>Title:</span>
          <input
            type="text"
            value={title}
            onChange={this.changeTitle}
            placeholder="Card Title"
          ></input>
          <span>Text:</span>
          <textarea
            type="text"
            value={text}
            onChange={this.changeText}
            placeholder="Card text"
          ></textarea>
          <span>Category Name:</span>
          <input
            type="text"
            value={categoryName}
            onChange={this.changeCategoryName}
            placeholder="Category Name"
          ></input>
          <button onClick={this.changeCard}>Create</button>
        </div>
      </article>
    );
  }
}

export default function CardChange(props) {
  let { id } = useParams();
  const navigation = useNavigate();

  return <CardChangeClass {...props} cardId={id} navigation={navigation} />;
}
