const Footer = () => {

    return (
        <footer className="footer">
            <div className="container footer-text">
                &copy; Copyright {new Date().getFullYear()}. Made by <a className="footer-text" href="https://chrisjamiecarter.github.io/">Chris Carter</a>
            </div>
        </footer>
    );
};

export default Footer;
