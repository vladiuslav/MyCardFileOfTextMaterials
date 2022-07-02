import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate, useParams } from "react-router-dom";
class CategoryChangeClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      categoryName: "",
    };

    this.changeCategoryName = this.changeCategoryName.bind(this);
    this.changeCategory = this.changeCategory.bind(this);
  }
  changeCategoryName(event) {
    this.setState({ categoryName: event.target.value });
  }
  changeCategory() {
    fetch(Variables.API_URL + "/Category", {
      method: "PUT",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
      body: JSON.stringify({
        id: parseInt(this.props.categoryId),
        name: this.state.categoryName,
      }),
    }).then((result) => {
      alert("Changed");
      this.props.navigation("/categories");
    });
  }
  render() {
    const { categoryName } = this.state;
    return (
      <article>
        <div className="loginForm">
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
          <button className="simpleButton" onClick={this.changeCategory}>
            Change category
          </button>
        </div>
      </article>
    );
  }
}

export default function CategoryChange(props) {
  let { id } = useParams();
  const navigation = useNavigate();

  return (
    <CategoryChangeClass {...props} categoryId={id} navigation={navigation} />
  );
}