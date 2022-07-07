import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate } from "react-router-dom";

class CardsClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cards: [],
      filter: 1,
      filterCategory: "",
    };

    this.setFilter1 = this.setFilter1.bind(this);
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
          .then((res) => {
            if (res.status !== 200) {
              alert("Something go wrong, please try again later.");
              return;
            }
            return res.json();
          })
          .then((value) => {
            this.setState({
              cards: value,
            });
          });

        break;
      case 3:
        let answerOk = false;
        if (this.state.filterCategory === "") {
          alert("Name is empty, please try again.");
          return;
        }
        fetch(Variables.API_URL + "/Card/GetCardsByCategory", {
          method: "POST",
          headers: {
            accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + sessionStorage.getItem("access_token"),
          },
          body: JSON.stringify({
            id: 0,
            name: this.state.filterCategory,
          }),
        })
          .then((res) => {
            if (res.status === 200) {
              answerOk = true;
              return res.json();
            } else if (res.status === 404) {
              alert("Category dont existed");
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
              cards: value,
            });
          });
        break;
      default:
        alert("Wrong sort category");
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
  setFilter3() {
    this.setState({ filter: 3 });
    this.refreshList(this.state.filter);
  }

  render() {
    const { navigation } = this.props;
    const { cards, filterCategory } = this.state;
    return (
      <article>
        <div className="sortButtons">
          <div>
            <input
              className="input"
              type="text"
              value={filterCategory}
              onChange={this.changeCategoryFilter}
              placeholder="Category name"
            ></input>
            <button className="simpleButton" onClick={this.setFilter3}>
              Sort by category
            </button>
          </div>
          <button className="simpleButton" onClick={this.setFilter1}>
            Reset list
          </button>
        </div>
        <div>
          <div className="cardsListItem">
            <ul>
              <li>Title</li>
              <li>Date:</li>
              <li>Creator name</li>
              <li>Category</li>
              <li>Text</li>
            </ul>
          </div>
          {cards.map((card) => {
            return (
              <div className="cardsListItem">
                <ul
                  key={card.id}
                  onClick={() => {
                    navigation("/Card/" + card.id);
                  }}
                >
                  <li>{card.title}</li>
                  <li>{card.creationDate.slice(0, 10)}</li>
                  <li>{card.userName}</li>
                  <li>{card.categoryName}</li>
                  <li>
                    {card.text.substr(0, 90) +
                      (card.text.length > 90 ? " ..." : "")}
                  </li>
                </ul>
              </div>
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
