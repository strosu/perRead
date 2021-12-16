import { Component } from "react";
import { Link } from 'react-router-dom';
import { useHistory } from 'react-router-dom';
import { Button, Card, CardBody, CardGroup, Col, Container, Input, InputGroup, InputGroupAddon, InputGroupText, Row, NavLink  } from 'reactstrap';

export class Articles extends Component {
    
    // routeChange=()=> {
    //     let path = `newPath`;
    //     let history = useHistory();
    //     history.push(path);
    // }
      
    constructor(props) {
        super(props);
        this.state = { isLoading: true, articles: [] };
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
                <h1 id="tabelLabel" >Articles</h1>
                <p>Loading articles...</p>
                {contents}
            </div>
        );
    }

    renderData() {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Author</th>
                            <th>Tags</th>
                            <th>Price</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.articles.map(article =>
                            <tr key={article.articleId}>
                                <td>{article.title}</td>
                                <td>{article.author.name}</td>
                                <td>{article.tags[0].tag.tagName}</td>
                                <td>{article.price}</td>
                                {/* <td>{article.summary}</td> */}
                            </tr>
                        )}
                    </tbody>
                </table>
                <Link to="/article/new" className="btn btn-primary">Add new Article</Link>
                {/* <Button color="primary" className="px-4"
                    onClick={routeChange}>
                    Login
                </Button> */}
            </div>
        );
    }

    async refreshData() {
        const response = await fetch('https://localhost:7176/article');
        const data = await response.json();
        this.setState({ articles: data, isLoading: false });
    }
}