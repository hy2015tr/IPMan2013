#=====================================================================================================================#

#!/usr/bin/perl
use strict;
use Infoblox;

#========================================================== CONNECTION ===============================================#

my $session = Infoblox::Session->new( master => "10.1.1.104", username => "webapi", password => "123456" );

if_error_occurs_abort();

#========================================================== GET_IP ===================================================#

my @ip_list = $session->get( object    => "Infoblox::DHCP::FixedAddr",
	 		 	                     mac       => "00:00:00:00:00:00",
	 			                     ipv4addr  => $ARGV[0] );

#========================================================== MODIFY_IP ================================================#

my $ip = $ip_list[0];

if($ip)
{

	  $ip -> extensible_attributes (
	                                {  
							        	            Site        => $ARGV[1], 
							        	            NOTE 				=> $ARGV[2], 
							        	            ServerName  => $ARGV[3], 
																		Responsible => $ARGV[4], 
																		ModulePort1 => $ARGV[5], 
																		ModulePort2 => $ARGV[6], 
																		Switch1			=> $ARGV[7], 
																		Switch2 		=> $ARGV[8], 
																	  VlanNo 			=> $ARGV[9], 
																	}
																);			       
			
		my $res_Modify = $session->modify($ip);	
			
	  if_error_occurs_abort();

}

else

#========================================================== CREATE_IP ================================================#

{

		my $new_ip = Infoblox::DHCP::FixedAddr ->new(
							    	    ipv4addr => $ARGV[0],
							          mac      => "00:00:00:00:00:00",
								        extensible_attributes => {  
															        	            Site        => $ARGV[1], 
															        	            NOTE 				=> $ARGV[2], 
															        	            ServerName  => $ARGV[3], 
																										Responsible => $ARGV[4], 
																										ModulePort1 => $ARGV[5], 
																										ModulePort2 => $ARGV[6], 
																										Switch1			=> $ARGV[7], 
																										Switch2 		=> $ARGV[8], 
																									  VlanNo 			=> $ARGV[9], 
																					       }
		                                           );
					 
		my $res_Create = $session->add($new_ip);
		
		if_error_occurs_abort();

}

#========================================================== SUCCESS ==================================================#

print "\nINFOBLOX_UPDATE_OK\n\n";

#========================================================== ERRORS ===================================================#

sub if_error_occurs_abort
{
	if ($session->status_code()) 
	{
	   die( "ERROR(", $session->status_code(). "): " . $session->status_detail() . "\n" );
	}
}

#=====================================================================================================================#