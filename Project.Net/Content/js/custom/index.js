const apiBaseOption = {
    baseURL: 'https://localhost:44381/api',
}
const baseOption = {
    baseURL: 'https://localhost:44381',
}
var header = new Vue({
    el: '#header',
    data() {
        return {
            listCategory: [],
            search: '',
            errors: '',
        }
    },
    created() {
        this.getCategory()
        this.infoUser()
    },
    methods: {
        getCategory() {
            axios.get('/Categories', apiBaseOption).then(response => {
                this.listCategory = response.data
                console.log('listcate', this.listCategory)
            }).catch((error) => {

            })
        },
        infoUser() {
            if (JSON.parse(localStorage.getItem("user"))) {
                return JSON.parse(localStorage.getItem("user"));
            }
            return null;
        },
        logout() {
            localStorage.removeItem("user")
            window.location.reload()
        }
    },
    computed: {
       
    },
})
const defaultForm = {
    UserName: '',
    Password:''
}
const defaultFormReg = {
    FullName: '',
    UserName: '',
    Password: '',
    Address: '',
    Email: '',
    GroupId:3,
    Phone: '',
    Gender: 1,
    Avatar: '',
    Status:1,
}
var login = new Vue({
    el: '#log_reg',
    data() {
        return {
            modelLogin: Object.assign({}, defaultForm),
            modelReg: Object.assign({}, defaultFormReg),
            repass:''
        }
    },
    created() {


    },
    methods: {
          login() {
            console.log('login ok')
            console.log('username', this.modelLogin.UserName)
            console.log('password', this.modelLogin.Password)
              axios.post('/api/Users/Login', this.modelLogin).then(async (response) => {
                localStorage.setItem("user", JSON.stringify(response.data))
                  console.log(JSON.parse(localStorage.getItem("user")))
                  Command: toastr["success"]("Log in successfully")
                  toastr.options = {
                      "closeButton": false,
                      "debug": false,
                      "newestOnTop": false,
                      "progressBar": false,
                      "positionClass": "toast-top-right",
                      "preventDuplicates": false,
                      "onclick": null,
                      "showDuration": "300",
                      "hideDuration": "1000",
                      "timeOut": "5000",
                      "extendedTimeOut": "1000",
                      "showEasing": "swing",
                      "hideEasing": "linear",
                      "showMethod": "fadeIn",
                      "hideMethod": "fadeOut"
                  }
                  $('#myModal').modal("hide")
                  setTimeout(() => {
                      window.location.reload()
                  },500)
              }).catch(() => {
				  this.errorlog("Account or password is incorrect")
            })
        },
        errorlog(err) {
            Command: toastr["error"](err)
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
        },
        register() {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            var phone =  /^0(1\d{9}|9\d{8})$/;
          if (this.modelReg.FullName == ''
              || this.modelReg.UserName == ''
              || this.modelReg.Password == ''
              || this.modelReg.Address == ''
              || this.repass == ''
              || this.modelReg.Email == ''
              || this.modelReg.Phone == '') {
			  this.errorlog("The field must not be not null")
          } else if (this.modelReg.Password != this.repass) {
              this.errorlog("Password and repassword must be same")
          }
          else if (!re.test(this.modelReg.Email)) {
			  this.errorlog("Email address is not valid");
          }
          else if (!phone.test(this.modelReg.Phone)) {
              this.errorlog("Phone number is not valid");
          }
          else {
              axios.get('Users/getUsername', { ...apiBaseOption, params: { username: this.modelReg.UserName } }).then(() => {
                  this.errorlog("Username is exist")
              }).catch(() => {
                  axios.post("/api/Users", this.modelReg).then((response) => {
                      localStorage.setItem("user", JSON.stringify(response.data))
					  Command: toastr["success"]("Sign Up Successfully")
                      toastr.options = {
                          "closeButton": false,
                          "debug": false,
                          "newestOnTop": false,
                          "progressBar": false,
                          "positionClass": "toast-top-right",
                          "preventDuplicates": false,
                          "onclick": null,
                          "showDuration": "300",
                          "hideDuration": "1000",
                          "timeOut": "5000",
                          "extendedTimeOut": "1000",
                          "showEasing": "swing",
                          "hideEasing": "linear",
                          "showMethod": "fadeIn",
                          "hideMethod": "fadeOut"
                      }
                      console.log(JSON.parse(localStorage.getItem("user")))
                      setTimeout(() => {
                          window.location.reload()
                      }, 500)
                  }).catch((err) => {
                      console.log(err)
                  })
              })
                
            }
        },
       
        
        
    }

})