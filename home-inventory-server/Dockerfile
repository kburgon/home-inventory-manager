FROM golang:1.23-bookworm as base
WORKDIR /build
COPY go.mod go.sum ./
RUN go mod download
COPY . .
RUN go build -o server
EXPOSE 8000
CMD ["/build/server"]
