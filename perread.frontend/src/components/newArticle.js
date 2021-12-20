import { Component } from "react";
import { Link } from 'react-router-dom';

export class NewArticle extends Component {
    constructor(props) {
        super(props);
        this.state = { newArticle: null };
        this.submitNewArticle = this.submitNewArticle.bind(this);
    }

    submitNewArticle() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: 'React POST Request Example', author: 'gogu', price: 22, content: 'first article content yay', tags: ['randomness2'] })
        };

        fetch('https://localhost:7176/article/create', requestOptions)
            .then(response => response.json())
            .then(data => this.setState({ newArticle: data }));

        console.log(this.state.newArticle)
    }

    render() {
        return (
            <div>
                <h1>Test</h1>
                <button onClick={this.submitNewArticle}>
                    Submit new article
                </button>

                {/* <Link to="/articles" className="btn btn-primary">Submit</Link> */}
            </div>
        );
    }
}