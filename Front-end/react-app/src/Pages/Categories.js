import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate } from "react-router-dom";

class CategoriesClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Categories: [],
      Nname: "",
    };
    this.refreshList = this.refreshList.bind(this);
    this.AddCategory = this.AddCategory.bind(this);
    this.changeCategoryName = this.changeCategoryName.bind(this);
  }

  changeCategoryName(event) {
    this.setState({ Nname: event.target.value });
  }
  AddCategory() {
    fetch(Variables.API_URL + "/Category", {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
      body: JSON.stringify({
        name: this.state.Nname,
      }),
    })
      .then((value) => {
        alert("New category added");
        this.setState({Nname:""});
        this.refreshList();
      });
  }

  refreshList(sort) {
    fetch(Variables.API_URL + "/Category", {
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
          Categories: value,
        });
      });
  }
  componentDidMount() {
    this.refreshList(this.state.filter);
  }

  render() {
    const { Categories, Nname } = this.state;
    return (
      <article>
        <div className="cardPage">
          <h1>Category names</h1>
          {Categories.map((category) => {
            return (
              <div key={category.id} className="cardsListItem">
                <p>{category.name}</p>
                <ChangeCategoryButton id={category.id} />
                <DeleteCategroyButton id={category.id} />
              </div>
            );
          })}
        </div>
        <div className="loginForm">
          <div className="inputDiv">
            <span>New category name: </span>
            <input
              className="input"
              value={Nname}
              onChange={this.changeCategoryName}
              placeholder="New category name"
            ></input>
          </div>

          <button className="simpleButton" onClick={this.AddCategory}>
            Add category
          </button>
        </div>
      </article>
    );
  }
}

function DeleteCategroyButton(props) {
  let deletefunction = function () {
    fetch(Variables.API_URL + "/Category/delete", {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
      body: props.id,
    }).then((result) => {
      window.location.reload(false);
      alert("Category deleted");
    });
  };

  return (
    <p
      onClick={() => {
        deletefunction();
      }}
      className="categoryButton"
    >
      Delete
    </p>
  );
}
function ChangeCategoryButton(props) {
  let navigation = useNavigate();
  return (
    <p
      onClick={() => {
        navigation("/categoryChange/" + props.id);
      }}
      className="categoryButton"
    >
      Change
    </p>
  );
}

export default function Categories(props) {
  const navigation = useNavigate();

  return <CategoriesClass {...props} navigation={navigation} />;
}
