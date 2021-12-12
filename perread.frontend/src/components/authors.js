import { Component } from "react";

export class Authors extends Component {
    constructor(props) {
        super(props);
        this.state = { isLoading: true, authors: [] };
    }

    componentDidMount() {
        this.refreshData();
    }

    render() {
        let contents = this.state.isLoading ?
            "Loading data, patience" :
            this.renderData();

        return (
            <div>
                <h1 id="tabelLabel" >Authors</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    renderData() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
              <thead>
                <tr>
                  <th>Date</th>
                  <th>Temp. (C)</th>
                  <th>Temp. (F)</th>
                  <th>Summary</th>
                </tr>
              </thead>
              <tbody>
                {this.state.authors.map(author =>
                  <tr key={author.date}>
                    <td>{author.date}</td>
                    <td>{author.temperatureC}</td>
                    <td>{author.temperatureF}</td>
                    <td>{author.summary}</td>
                  </tr>
                )}
              </tbody>
            </table>
          );
    }

    async refreshData() {
        const response = await fetch('https://localhost:7176/WeatherForecast');
        const data = await response.json();
        this.setState({ authors: data, isLoading: false });
    }
}