import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate } from "react-router-dom";

class CardsClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cards: [],
      filter: 1,
      filterCategory: 0,
    };

    this.setFilter1 = this.setFilter1.bind(this);
    this.setFilter2 = this.setFilter2.bind(this);
    this.setFilter3 = this.setFilter3.bind(this);
    this.changeCategoryFilter = this.changeCategoryFilter.bind(this);
  }

  refreshList(sort) {
    switch (sort) {
      case 1:
        fetch(Variables.API_URL + "/Card", {
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
              cards: value,
            });
          });

        break;
      case 2:
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
            this.setState({
              cards: value,
            });
          });
        break;
      case 3:
        fetch(
          Variables.API_URL +
            "/GetCardsByCategory/" +
            this.state.filterCategory,
          {
            method: "GET",
            headers: {
              accept: "application/json",
              "Content-Type": "application/json",
              Authorization: "Bearer " + sessionStorage.getItem("access_token"),
            },
          }
        )
          .then((res) => res.json())
          .then((value) => {
            this.setState({
              cards: value,
            });
          });
        break;
      default:
        console.log("Problem with switch");
        break;
    }

    
  }
  componentDidMount() {
    this.refreshList(this.state.filter);
  }

  changeCategoryFilter(event) {
    this.setState({ filterCategory: event.target.value });
  }
  setFilter1() {
    this.setState({ filter: 1 });
    this.refreshList(this.state.filter);
  }
  setFilter2() {
    this.setState({ filter: 2 });
    this.refreshList(this.state.filter);
  }
  setFilter3() {
    this.setState({ filter: 3 });
    this.refreshList(this.state.filter);
  }

  render() {
    const { navigation } = this.props;
    const { cards, filterCategory } = this.state;
    return (
      <article>
        <div>
          <ul>
            <li>Id</li>
            <li>Title</li>
            <li>Likes</li>
            <li>Category name</li>
            <li>User name</li>
            <li>Text</li>
            <li>
              <button onClick={this.setFilter2}>sortByPopularity</button>
            </li>
            <li>
              <input
                type="text"
                value={filterCategory}
                onChange={this.changeCategoryFilter}
                placeholder="CategoryId"
              ></input>
              <button onClick={this.setFilter2}>sortByCategory</button>
            </li>
            <li>
              <button onClick={this.setFilter1}>resetList</button>
            </li>
          </ul>
          {cards.map((card) => {
            return (
              <ul
                key={card.id}
                onClick={() => {
                  navigation("/Card/" + card.id);
                }}>
                <li>{card.id}</li>
                <li>{card.title}</li>
                <li>{card.numberOfLikes}</li>
                <li>{card.categoryId}</li>
                <li>{card.}</li>
                <li>
                  {card.text.substr(0, 90) +
                    (card.text.length > 90 ? " ..." : "")}
                </li>
              </ul>
            );
          })}
        </div>
      </article>
    );
  }
}

export default function Cards(props) {
  const navigation = useNavigate();

  return <CardsClass {...props} navigation={navigation} />;
}