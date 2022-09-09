package main
import (
    "net/http"
	"strconv"

	"github.com/gin-gonic/gin"
)

func main() {
    router := gin.Default()
    router.GET("/compute", computeFib)

    router.Run("localhost:5004")
}

func computeFib(c *gin.Context) {
	n, err := strconv.Atoi(c.Query("n"))
    if err != nil {
        return
    }

	a := 0
    b := 1
	result := 0

	if n == 0 {
		result = a
	} else {
		for i := 2; i <= n; i++ {
			c := a + b
			a = b
			b = c	
		}
		result = b
	}

	c.Writer.Header().Set("stack", "go")
	resultJson := map[string]int{"Result": result}
    c.IndentedJSON(http.StatusOK, resultJson)
}