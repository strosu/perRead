import { Component } from "react";
import { Link } from 'react-router-dom';
import { useHistory } from 'react-router-dom';
import { Button, Card, CardBody, CardGroup, Col, Container, Input, InputGroup, InputGroupAddon, InputGroupText, Row, NavLink } from 'reactstrap';

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
                                <td>{this.formatAuthors(article.authors)}</td>
                                <td>{article.tags[0]?.tagName}</td>
                                <td>{article.price}</td>
                                {/* <td>{article.summary}</td> */}
                                <td>
                                    <Link to={`article/${article.articleId}`}>Read</Link>
                                    {/* <Button onClick={() => this.clickRead(article.articleId)} className="domain-button" type='primary'>Read</Button> */}
                                </td>
                                <td>
                                    <Button onClick={() => this.clickDelete(article.articleId)} className="domain-button" type='primary'>Delete</Button>
                                </td>
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

    formatAuthors(authorInfos) {
        return(
            <table>
                <tbody>
                    <tr>
                    {authorInfos.map(authorInfo =>
                        <td>
                            {/* <Link to={`article/${article.articleId}`}>Read</Link> */}
                            <Link to={`author/${authorInfo.authorId}`}>{authorInfo.name} </Link>
                        </td>)}
                    </tr>
                </tbody>
            </table>
        );
    }

    async refreshData() {
        const response = await fetch('https://localhost:7176/article');
        const data = await response.json();
        this.setState({ articles: data, isLoading: false });
    }

    async clickDelete(articleId) {
        const requestOptions = {
            method: 'DELETE',
        };

        await fetch(`https://localhost:7176/article/${articleId}`, requestOptions);
        // .then(response => response.json())
        // .then(data => this.setState({ newArticle: data }));

        await this.refreshData();
    }

    async clickRead(articleId) {
        
        this.setState({ redirect: `/articles/${articleId}` });
    }
}