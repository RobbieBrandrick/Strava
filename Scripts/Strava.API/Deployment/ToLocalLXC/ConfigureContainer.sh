DeployToWWWLocation=/var/www
DeployToLocation=$DeployToWWWLocation/StravaAPI
ExecStart=$DeployToLocation/Strava.API

echo "Updating Container and adding required programs"
echo "deb [trusted=yes] https://nginx.org/packages/ubuntu/ focal nginx" | tee -a /etc/apt/sources.list
echo "deb-src [trusted=yes] https://nginx.org/packages/ubuntu/ focal  nginx" | tee -a /etc/apt/sources.list
apt-get update
apt-get -y install nano wget curl zip nginx

wget https://dot.net/v1/dotnet-install.sh
bash ./dotnet-install.sh -c Current -version latest --install-dir /usr/share/dotnet  --runtime dotnet

echo "Unzipping Deployment"
mkdir $DeployToWWWLocation
unzip -q /Deployment.zip -d $DeployToLocation/
echo 'Setting up nginx'

service nginx start

echo "    server {
        listen        80;
        server_name   stravalocal.com *.stravalocal.com;
        location / {
            proxy_pass         http://localhost:5000;
            proxy_http_version 1.1;
            proxy_set_header   Upgrade \$http_upgrade;
            proxy_set_header   Connection keep-alive;
            proxy_set_header   Host \$host;
            proxy_cache_bypass \$http_upgrade;
            proxy_set_header   X-Forwarded-For \$proxy_add_x_forwarded_for;
            proxy_set_header   X-Forwarded-Proto \$scheme;
        }
    }
    server {
        listen   80 default_server;
        # listen [::]:80 default_server deferred;
        return   444;
    }" | tee /etc/nginx/sites-available/default

nginx -t

nginx -s reload

echo "127.0.0.1       stravalocal.com" | tee -a /etc/hosts

echo 'Starting Deployment'
cd $DeployToLocation && $ExecStart > log &

echo 'Sleeping for Deployment startup'
sleep 5

echo 'Testing Deployment'

curl http://localhost:5000
curl stravalocal.com

echo 'Setting Up Deployment in systemd'

echo "    [Unit]
    Description=Run Kestrel Service for Strava website

    [Service]
    WorkingDirectory=$DeployToLocation
    ExecStart=$ExecStart
    Restart=always
    # Restart service after 10 seconds if the dotnet service crashes:
    RestartSec=10
    KillSignal=SIGINT
    SyslogIdentifier=dotnet-example
    User=www-data
    Environment=ASPNETCORE_ENVIRONMENT=development
    Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
    User=root
    Group=root

    [Install]
    WantedBy=multi-user.target" | tee /etc/systemd/system/strava-kestrel.service

sudo systemctl enable strava-kestrel.service
sudo systemctl start strava-kestrel.service
sudo systemctl status strava-kestrel.service
