import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate, useParams } from "react-router-dom";

class CardChangeClass extends Component {
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
      .then((value) => {
        if (!answerOk) {
          return;
        }
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
          .then((res) => {
            if (!answerOk) {
              return;
            } else {
              answerOk = false;
            }
            if (res.status === 200) {
              answerOk = true;
              return res.json();
            } else if (res.status === 401) {
              alert("You was unauthorized please login again.");
              this.props.navigation("/login");
              window.location.reload(false);
              return;
            } else if (res.status === 404) {
              alert("Category not found");
              this.props.navigation("/cards");
              return;
            } else {
              alert("Something go wrong, please try again later.");
              return;
            }
          })
          .then((value) => {
            if (!answerOk) {
              return;
            }
            this.setState({
              categoryName: value,
            });
          });
      });
  }

  changeCard() {
    let answerOk = false;
    fetch(Variables.API_URL + "/Card", {
      method: "PUT",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
      body: JSON.stringify({
        id: parseInt(this.props.cardId),
        title: this.state.title,
        text: this.state.text,
        categoryName: this.state.categoryName,
      }),
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
          alert("You cant edit this card, not enough rights");
          return;
        } else if (res.status === 400) {
          alert("Wrong input");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
      .then((reslt) => {
        if (!answerOk) {
          return;
        }
        alert("Card changed");
        this.props.navigation("/Card/" + this.props.cardId);
      });
  }
  render() {
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
          <button className="simpleButton" onClick={this.changeCard}>
            Change
          </button>
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
