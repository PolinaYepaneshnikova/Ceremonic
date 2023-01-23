import logo from './logo.svg';
import './App.css';
import { Component } from 'react';

const address = 'https://ceremonicapi.azurewebsites.net';
// const address = '';

class App extends Component {
  constructor() {
    super();

    this.state = {
      users: [],
    };
  }

  getUsers = async () => {
    fetch(address + '/api/users')
    .then(async response => {
      const users = await response.json();

      this.setState({
        users: users,
      });
    });
  }

  render() {
    const usersLi = this.state.users.map((item, index) => <li key={index}>{item.name}</li>);

    return (
      <div className='App'>
        <button onClick={this.getUsers}>Загрузить список пользователей</button>
        <ul>{usersLi}</ul>
      </div>
    );
  }
}

export default App;
