export default {
  // after dom render ready
  window.onload = function () {
    const tagA = document.getElementsByClassName("navbar-brand")[0];
    tagA.setAttribute("href", "/");
    document.getElementsByClassName("actionbar")[0].style.display = "none";
  }

}
